using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Saitynas1Lab.Data.Dtos.Posts;
using Saitynas1Lab.Data.Dtos.Reviews;
using Saitynas1Lab.Data.Entities;
using Saitynas1Lab.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saitynas1Lab.Auth.Model;

namespace Saitynas1Lab.Controllers
{
    [ApiController]
    [Route("api/posts/{id}/reviews")]
    public class ReviewController: ControllerBase
    {
        private readonly IReviewsRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IPostsRepository _PostsRepository;
        private readonly IAuthorizationService _authorizationService;

        public ReviewController(IReviewsRepository reviewRepository, IMapper mapper, IPostsRepository postsRepository, IAuthorizationService authorizationService)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _PostsRepository = postsRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<Review>> GetAllAsync(int id)
        {
            
            var posts = await _reviewRepository.GetAsync(id);
            Console.WriteLine("praejooooom");
            //return posts;
            //return new List<Review>
            //{
            //    new Review()
            //{
            //    Initiator = "inic",
            //    Title = "title",
            //    Body = "body",
            //    CreationDateUtc = DateTime.UtcNow

            //},
            //    new Review()
            //{
            //    Initiator = "inic",
            //    Title = "title",
            //    Body = "body",
            //    CreationDateUtc = DateTime.UtcNow

            //} };
            return posts;
            //return posts.Select(o => _mapper.Map<ReviewDto>(o));
        }

        // /api/topics/1/posts/2
        [HttpGet("{postId}")]
        public async Task<ActionResult<ReviewDto>> GetAsync(int postId, int reviewId)
        {
            var post = await _reviewRepository.GetAsync(postId, reviewId);
            if (post == null) return NotFound();

            return Ok(_mapper.Map<ReviewDto>(post));
        }

        [HttpPost]
        public async Task<ActionResult<ReviewDto>> PostAsync(int id, CreateReviewDto reviewDto)
        {
            var userId = User.Claims.ToList()[2].Value;
            var post = await _PostsRepository.Get(id);
            if (post == null) return NotFound($"Couldn't find a post with id of {id}");
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest("Negalite sukurti įrašo neprisijungęs. Prisijunkite");
            }
            

            //var authorizationResult = await _authorizationService.AuthorizeAsync(User, post, PolicyNames.SameUser);
            //if (!authorizationResult.Succeeded && !User.IsInRole(DemoRestUserRoles.Admin))
            //{
            //    //403 or 404
            //    return Forbid();
            //}

            var review = _mapper.Map<Review>(reviewDto);
            review.PostId = id;
            review.Initiator = User.Identity.Name;
            review.UserId = userId;
            await _reviewRepository.InsertAsync(review);

            return Created($"/api/posts/{id}/reviews/{review.Id}", _mapper.Map<ReviewDto>(review));
        }

        [HttpPut("{reviewId}")]
        public async Task<ActionResult<ReviewDto>> PutAsync(int id, int reviewId, UpdateReviewDto reviwDto)
        {
            var post = await _PostsRepository.Get(id);
            if (post == null) return NotFound($"Couldn't find a post with id of {id}");
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest("Negalite sukurti įrašo neprisijungęs. Prisijunkite");
            }
           

            
            var review = await _reviewRepository.GetAsync(id, reviewId);
            if (review == null)
                return NotFound();
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, review, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded && !User.IsInRole(DemoRestUserRoles.Admin))
            {
                //403 or 404
                return Forbid();
            }
            //oldPost.Body = postDto.Body;
            _mapper.Map(reviwDto, review);
            

            await _reviewRepository.UpdateAsync(review);

            return Ok(_mapper.Map<ReviewDto>(review));
        }

        [HttpDelete("{reviewId}")]
        public async Task<ActionResult> DeleteAsync(int id, int reviewId)
        {
            var post = await _PostsRepository.Get(id);
            if (post == null)
                return NotFound();
            var review = await _reviewRepository.GetAsync(id, reviewId);
            if (review == null)
                return NotFound();
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest("Negalite sukurti įrašo neprisijungęs. Prisijunkite");
            }
            
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, review, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded && !User.IsInRole(DemoRestUserRoles.Admin))
            {
                //403 or 404
                return Forbid();
            }
            await _reviewRepository.DeleteAsync(review);

            // 204
            return NoContent();
        }
    }
}
