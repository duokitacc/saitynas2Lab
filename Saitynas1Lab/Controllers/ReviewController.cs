using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Saitynas1Lab.Data.Dtos.Posts;
using Saitynas1Lab.Data.Dtos.Reviews;
using Saitynas1Lab.Data.Entities;
using Saitynas1Lab.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas1Lab.Controllers
{
    [ApiController]
    [Route("api/posts/{id}/reviews")]
    public class ReviewController: ControllerBase
    {
        private readonly IReviewsRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IPostsRepository _PostsRepository;

        public ReviewController(IReviewsRepository reviewRepository, IMapper mapper, IPostsRepository postsRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _PostsRepository = postsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Review>>> GetAllAsync(int id)
        {

            var post = await _PostsRepository.Get(id);
            if (post == null)
            {
                return NotFound($"Post with id '{id}' not found.");
            }
            var reviews = await _reviewRepository.GetAllAsync(id);
            if (reviews == null)
            {
                return NotFound($"Ingredient with id '{reviews}' not found.");
            }


            return Ok(reviews.Select(o => _mapper.Map<Review>(o)));
        }

        // /api/topics/1/posts/2
        [HttpGet("{reviewId}")]
        public async Task<ActionResult<ReviewDto>> GetAsync(int id, int reviewId)
        {

            var reviews = await _reviewRepository.GetAsync(id, reviewId);
            if (reviews == null)
            {
                return NotFound($"Reviews with post  id '{id}' and review id '{reviewId}'not found.");
            }
           
            return Ok(_mapper.Map<Review>(reviews));
        }

        [HttpPost]
        public async Task<ActionResult<ReviewDto>> PostAsync(int id, CreateReviewDto reviewDto)
        {
            var post = await _PostsRepository.Get(id);
            if (post == null) return NotFound($"Couldn't find a post with id of {id}");

            var review = _mapper.Map<Review>(reviewDto);
            review.PostId = id;

            await _reviewRepository.InsertAsync(review);

            return Created($"/api/posts/{id}/reviews/{review.Id}", _mapper.Map<PostDto>(post));
        }

        [HttpPut("{reviewId}")]
        public async Task<ActionResult<ReviewDto>> PostAsync(int id, int reviewId, UpdateReviewDto reviwDto)
        {
            var topic = await _PostsRepository.Get(id);
            if (topic == null) return NotFound($"Couldn't find a post with id of {id}");

            var oldPost = await _reviewRepository.GetAsync(id, reviewId);
            if (oldPost == null)
                return NotFound();

            //oldPost.Body = postDto.Body;
            _mapper.Map(reviwDto, oldPost);

            await _reviewRepository.UpdateAsync(oldPost);

            return Ok(_mapper.Map<ReviewDto>(oldPost));
        }

        [HttpDelete("{reviewId}")]
        public async Task<ActionResult> DeleteAsync(int id, int reviewId)
        {
            var post = await _reviewRepository.GetAsync(id, reviewId);
            if (post == null)
                return NotFound();

            await _reviewRepository.DeleteAsync(post);
            
            // 204
            return NoContent();
        }
    }
}
