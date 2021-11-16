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
        public async Task<IEnumerable<Review>> GetAllAsync(int postId)
        {
            
            var posts = await _reviewRepository.GetAsync(postId);
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
        public async Task<ActionResult<ReviewDto>> PostAsync(int postId, CreateReviewDto reviewDto)
        {
            var post = await _PostsRepository.Get(postId);
            if (post == null) return NotFound($"Couldn't find a post with id of {postId}");

            var review = _mapper.Map<Review>(reviewDto);
            review.PostId = postId;

            await _reviewRepository.InsertAsync(review);

            return Created($"/api/posts/{postId}/reviews/{review.Id}", _mapper.Map<PostDto>(post));
        }

        [HttpPut("{reviewId}")]
        public async Task<ActionResult<ReviewDto>> PostAsync(int postId, int reviewId, UpdateReviewDto reviwDto)
        {
            var topic = await _PostsRepository.Get(postId);
            if (topic == null) return NotFound($"Couldn't find a post with id of {postId}");

            var oldPost = await _reviewRepository.GetAsync(postId, reviewId);
            if (oldPost == null)
                return NotFound();

            //oldPost.Body = postDto.Body;
            _mapper.Map(reviwDto, oldPost);

            await _reviewRepository.UpdateAsync(oldPost);

            return Ok(_mapper.Map<ReviewDto>(oldPost));
        }

        [HttpDelete("{reviewId}")]
        public async Task<ActionResult> DeleteAsync(int postId, int reviewId)
        {
            var post = await _reviewRepository.GetAsync(postId, reviewId);
            if (post == null)
                return NotFound();

            await _reviewRepository.DeleteAsync(post);

            // 204
            return NoContent();
        }
    }
}
