using Microsoft.EntityFrameworkCore;
using Saitynas1Lab.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas1Lab.Data.Repositories
{
    public interface IReviewsRepository
    {
        Task DeleteAsync(Review review);
        Task<List<Review>> GetAllAsync(int postId);
        Task<Review> GetAsync(int postId, int reviewId);
        Task InsertAsync(Review review);
        Task UpdateAsync(Review review);
    }


    //public interface IReviewsRepository
    //{
    //    Task DeleteAsync(Review review);
    //    Task<List<Review>> GetAsync(int postId);
    //    Task<Review> GetAsync(int postId, int reviewId);
    //    Task InsertAsync(Review review);
    //    Task UpdateAsync(Review review);
    //}



    public class ReviewsRepository : IReviewsRepository
    {
        private readonly DemoRestContext _demoRestContext;

        public ReviewsRepository(DemoRestContext demoRestContext)
        {
            _demoRestContext = demoRestContext;
        }
        public async Task<Review> GetAsync(int postId, int reviewId)
        {
            
            return await _demoRestContext.Reviews.FirstOrDefaultAsync(o => o.Id == postId && o.Post.Id == reviewId);
            //return new Review()
            //{
            //    Initiator = "inic",
            //    Title = "title",
            //    Body = "body",
            //    CreationDateUtc = DateTime.UtcNow

            //};
        }

        public async Task<List<Review>> GetAllAsync(int postId)
        {
            return await _demoRestContext.Reviews.Where(o => o.PostId == postId).ToListAsync();

            
        }

        public async Task InsertAsync(Review review)
        {
            _demoRestContext.Reviews.Add(review);
            
            await _demoRestContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Review review)
        {
            _demoRestContext.Reviews.Update(review);
            
            await _demoRestContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Review review)
        {
            _demoRestContext.Reviews.Remove(review);

            
            await _demoRestContext.SaveChangesAsync();
        }
    }
}
