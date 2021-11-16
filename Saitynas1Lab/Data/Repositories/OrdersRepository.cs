using Saitynas1Lab.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas1Lab.Data.Repositories
{
    

    public interface IOrdersRepository
    {
        Task<Order> Create(Order order);
        Task<Order> Delete(Order post);
        Task<Order> Get(int id);
        Task<IEnumerable<Order>> GetAll();
        Task<Order> Put(Order post);
    }

    public class OrdersRepository : IOrdersRepository
    {
        private readonly DemoRestContext _demoRestContext;

        public OrdersRepository(DemoRestContext demoRestContext)
        {
            _demoRestContext = demoRestContext;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return new List<Order>
            {
                new Order()
                {
                    Id = 0,
                    GameName = "name",
                    Price = 225,

                    Body = "body",
                    CreationDateUtc = DateTime.UtcNow
                },
                new Order()
                {
                    Id = 0,
                    GameName = "name",
                    Price = 225,

                    Body = "body",
                    CreationDateUtc = DateTime.UtcNow
        }
            };
        }

        public async Task<Order> Get(int id)
        {
            return new Order()
            {
                Id = 0,
                GameName = "name",
                Price = 225,

                Body = "body",
                CreationDateUtc = DateTime.UtcNow
            };
        }

        public async Task<Order> Create(Order order)
        {
            _demoRestContext.Orders.Add(order);
            //await _demoRestContext.SaveChangesAsync();
            //Response.StatusCode = (int)HttpStatusCode.Created;

            return order;
        }

        public async Task<Order> Put(Order post)
        {
            return new Order()
            {
                Id = 0,
                GameName = "name",
                Price = 225,

                Body = "body",
                CreationDateUtc = DateTime.UtcNow
            };
        }

        public async Task<Order> Delete(Order post)
        {
            return post;
        }
    }
}
