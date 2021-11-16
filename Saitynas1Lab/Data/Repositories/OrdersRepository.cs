using Saitynas1Lab.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas1Lab.Data.Repositories
{
    

    public interface IOrdersRepository
    {
        Task Create(Order order);
        Task Delete(Order post);
        Task<Order> Get(int id);
        Task<List<Order>> GetAll();
        Task Put(Order post);
    }

    public class OrdersRepository : IOrdersRepository
    {
        private readonly DemoRestContext _demoRestContext;

        public OrdersRepository(DemoRestContext demoRestContext)
        {
            _demoRestContext = demoRestContext;
        }

        public async Task<List<Order>> GetAll()
        {
            return await _demoRestContext.Orders.ToListAsync();
           

        }

        public async Task<Order> Get(int id)
        {
           
            return await _demoRestContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task Create(Order order)
        {
            _demoRestContext.Orders.Add(order);
            await _demoRestContext.SaveChangesAsync();
            

            
        }

        public async Task Put(Order order)
        {
            _demoRestContext.Orders.Update(order);
            await _demoRestContext.SaveChangesAsync();
            
        }

        public async Task Delete(Order post)
        {
            _demoRestContext.Orders.Remove(post);
            await _demoRestContext.SaveChangesAsync();
        }
    }
}
