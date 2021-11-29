using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Saitynas1Lab.Data.Dtos.Posts;
using Saitynas1Lab.Data.Entities;
using Saitynas1Lab.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Saitynas1Lab.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IMapper _mapper;
        //private readonly ITopicsRepository _topicsRepository;

        public PostsController(IPostsRepository postsRepository, IMapper mapper)
        {
            _postsRepository = postsRepository;
            _mapper = mapper;
           
        }

        [HttpGet]
        public async Task<IEnumerable<PostDto>> GetAll()
        {
            
            return (await _postsRepository.GetAll()).Select(o => _mapper.Map<PostDto>(o));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> Get(int id)
        {
            var topic = await _postsRepository.Get(id);
            if (topic == null) return NotFound($"Topic with id '{id}' not found.");

            //return _mapper.Map<TopicDto>(topic);
            return Ok(_mapper.Map<PostDto>(topic));
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> Post(CreatePostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            
            await _postsRepository.Create(post);
            
            // 201
            // Created topic
            return Created($"/api/posts/{post.Id}", _mapper.Map<PostDto>(post));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PostDto>> Put(int id, UpdatePostDto topicDto)
        {
            var post = await _postsRepository.Get(id);
            if (post == null) return NotFound($"Topic with id '{id}' not found.");

            //topic.Name = topicDto.Name;
            _mapper.Map(topicDto, post);

            await _postsRepository.Put(post);

            //return _mapper.Map<TopicDto>(topic);
            return Ok(_mapper.Map<PostDto>(post));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PostDto>> Delete(int id)
        {
            var post = await _postsRepository.Get(id);
            if (post == null) return NotFound($"Topic with id '{id}' not found.");

            await _postsRepository.Delete(post);

            // 204
            return NoContent();
        }
    }
}
