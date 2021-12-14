using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using Microsoft.AspNet.Identity;
using Saitynas1Lab.Data.Dtos.Posts;
using Saitynas1Lab.Data.Entities;
using Saitynas1Lab.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Saitynas1Lab.Auth.Model;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;

namespace Saitynas1Lab.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        //private readonly ITopicsRepository _topicsRepository;

        public PostsController(IPostsRepository postsRepository, IMapper mapper,IAuthorizationService authorizationService)
        {
            _postsRepository = postsRepository;
            _mapper = mapper;
            _authorizationService = authorizationService;
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
        //[Authorize(Roles = DemoRestUserRoles.SimpleUser + DemoRestUserRoles.Admin)]
        public async Task<ActionResult<PostDto>> Post(CreatePostDto postDto)
        {
            //var userId = User.Identity.GetUserId();
            //var userId = User.Claims.Last().Value;
            
            
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest("Negalite sukurti įrašo neprisijungęs. Prisijunkite");
            }
            var userId = User.Claims.ToList()[2].Value;
            var post = _mapper.Map<Post>(postDto);
            Console.WriteLine("UserID:  " + userId);

            post.UserId = userId;
            
            await _postsRepository.Create(post);
            
            // 201
            // Created topic
            return Created($"/api/posts/{post.Id}", _mapper.Map<PostDto>(post));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PostDto>> Put(int id, UpdatePostDto postDto)
        {
            var post = await _postsRepository.Get(id);
            if (post == null) return NotFound($"Posts with id '{id}' not found.");
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest("Negalite sukurti įrašo neprisijungęs. Prisijunkite");
            }
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, post, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded && !User.IsInRole(DemoRestUserRoles.Admin))
            {
                //403 or 404
                return Forbid();
            }
            //topic.Name = topicDto.Name;
            _mapper.Map(postDto, post);

            await _postsRepository.Put(post);

            //return _mapper.Map<TopicDto>(topic);
            return Ok(_mapper.Map<PostDto>(post));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PostDto>> Delete(int id)
        {
            var post = await _postsRepository.Get(id);
            if (post == null) return NotFound($"Post with id '{id}' not found.");
            if(!User.Identity.IsAuthenticated)
            {
                return BadRequest("Negalite ištrinti įrašo neprisijungęs. Prisijunkite");
            }
            //
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, post, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded && !User.IsInRole(DemoRestUserRoles.Admin))
            {
                //403 or 404
                return Forbid();
            }
            //topic.Name = topicDto.Name;
            await _postsRepository.Delete(post);

            // 204
            return NoContent();
        }
    }
}
