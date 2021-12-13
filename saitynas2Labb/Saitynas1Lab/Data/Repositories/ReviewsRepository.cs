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
        Task<Review> DeleteAsync(Review review);
        Task<List<Review>> GetAsync(int postId);
        Task<Review> GetAsync(int postId, int reviewId);
        Task<Review> InsertAsync(Review review);
        Task<Review> UpdateAsync(Review review);
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
            //return await _demoRestContext.Reviews.FirstOrDefaultAsync(o => o.PostId == postId && o.Id == reviewId);
            return new Review()
            {
                Initiator = "inic",
                Title = "title",
                Body = "body",
                CreationDateUtc = DateTime.UtcNow

            };
        }

        public async Task<List<Review>> GetAsync(int postId)
        {
            //return await _demoRestContext.Reviews.Where(o => o.PostId == postId).ToListAsync();

            return new List<Review>
            {
                new Review()
            {
                Initiator = "inic",
                Title = "title",
                Body = "body",
                CreationDateUtc = DateTime.UtcNow

            },
                new Review()
            {
                Initiator = "inic",
                Title = "title",
                Body = "body",
                CreationDateUtc = DateTime.UtcNow

            } };

        }

        public async Task<Review> InsertAsync(Review review)
        {
            _demoRestContext.Reviews.Add(review);
            return review;
            //await _demoRestContext.SaveChangesAsync();
        }

        public async Task<Review> UpdateAsync(Review review)
        {
            _demoRestContext.Reviews.Update(review);
            return review;
            //await _demoRestContext.SaveChangesAsync();
        }

        public async Task<Review> DeleteAsync(Review review)
        {
            //_demoRestContext.Reviews.Remove(review);

            return review;
            //await _demoRestContext.SaveChangesAsync();
        }
    }
}
