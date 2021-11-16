using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saitynas1Lab.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Azure;

namespace Saitynas1Lab.Data.Repositories
{
    public interface IPostsRepository
    {
        Task<Post> Create(Post post);
        Task<Post> Delete(Post post);
        Task<Post> Get(int id);
        Task<IEnumerable<Post>> GetAll();
        Task<Post> Put(Post post);
    }

    //public interface IPostsRepository
    //{
    //    Task<Post> Create(Post topic);
    //    Task Delete(Post topic);
    //    Task<Post> Get(int id);
    //    Task<IEnumerable<Post>> GetAll();
    //    Task<Post> Put(Post topic);
    //}

    //public interface IPostsRepository
    //{
    //    Task<Post> GetAsync(int topicId, int postId);
    //    Task<List<Post>> GetAsync(int topicId);
    //    Task InsertAsync(Post post);
    //    Task UpdateAsync(Post post);
    //    Task DeleteAsync(Post post);
    //}

    public class PostsRepository : IPostsRepository
    {
        private readonly DemoRestContext _demoRestContext;

        public PostsRepository(DemoRestContext demoRestContext)
        {
            _demoRestContext = demoRestContext;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return new List<Post>
            {
                new Post()
                {
                    Id = 0,
                    GameName = "name",
                    Price = 225,

                    Body = "body",
                    CreationDateUtc = DateTime.UtcNow
                },
                new Post()
                {
                    Id = 0,
                    GameName = "name",
                    Price = 225,

                    Body = "body",
                    CreationDateUtc = DateTime.UtcNow
        }
            };
        }

        public async Task<Post> Get(int id)
        {
            return new Post()
            {
                Id = 0,
                GameName = "name",
                Price = 225,

                Body = "body",
                CreationDateUtc = DateTime.UtcNow
            };
        }

        public async Task<Post> Create(Post post)
        {
            _demoRestContext.Posts.Add(post);
            //await _demoRestContext.SaveChangesAsync();
            //Response.StatusCode = (int)HttpStatusCode.Created;

            return post;
        }

        public async Task<Post> Put(Post post)
        {
            return new Post()
            {
                Id = 0,
                GameName = "name",
                Price = 225,

                Body = "body",
                CreationDateUtc = DateTime.UtcNow
            };
        }

        public async Task<Post> Delete(Post post)
        {
            return post;
        }
    }
}
