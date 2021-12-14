using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saitynas1Lab.Auth.Model;
using Saitynas1Lab.Data.Dtos.Orders;
using Saitynas1Lab.Data.Entities;
using Saitynas1Lab.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas1Lab.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {

        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;
        

        public OrdersController(IOrdersRepository ordersRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IEnumerable<OrderDto>> GetAll()
        {
            return (await _ordersRepository.GetAll()).Select(o => _mapper.Map<OrderDto>(o));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            var topic = await _ordersRepository.Get(id);
            if (topic == null) return NotFound($"Orders with id '{id}' not found.");

            //return _mapper.Map<TopicDto>(topic);
            return Ok(_mapper.Map<OrderDto>(topic));
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Post(CreateOrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);

            await _ordersRepository.Create(order);

            // 201
            // Created topic
            return Created($"/api/orders/{order.Id}", _mapper.Map<OrderDto>(order));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = DemoRestUserRoles.Admin)]
        public async Task<ActionResult<OrderDto>> Put(int id, UpdateOrderDto topicDto)
        {
            var order = await _ordersRepository.Get(id);
            if (order == null) return NotFound($"Orders with id '{id}' not found.");

            //topic.Name = topicDto.Name;
            _mapper.Map(topicDto, order);

            await _ordersRepository.Put(order);

            //return _mapper.Map<TopicDto>(topic);
            return Ok(_mapper.Map<OrderDto>(order));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = DemoRestUserRoles.Admin)]
        public async Task<ActionResult<OrderDto>> Delete(int id)
        {
            var post = await _ordersRepository.Get(id);
            if (post == null) return NotFound($"Orders with id '{id}' not found.");

            await _ordersRepository.Delete(post);

            // 204
            return NoContent();
        }
    }
}
