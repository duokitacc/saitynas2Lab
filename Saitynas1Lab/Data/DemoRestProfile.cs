using AutoMapper;
using Saitynas1Lab.Data.Dtos.Posts;
using Saitynas1Lab.Data.Dtos.Reviews;

using Saitynas1Lab.Data.Dtos.Orders;
using Saitynas1Lab.Data.Entities;

namespace Saitynas1Lab.Data
{
    public class DemoRestProfile : Profile
    {
        public DemoRestProfile()
        {
            

            CreateMap<CreatePostDto, Post>();
            CreateMap<UpdatePostDto, Post>();
            CreateMap<Post, PostDto>();

            CreateMap<CreateReviewDto, Review>();
            CreateMap<UpdateReviewDto, Review>();
            CreateMap<Review, ReviewDto>();


            CreateMap<CreateOrderDto, Order>();
            CreateMap<UpdateOrderDto, Order>();
            CreateMap<Order, OrderDto>();
        }
    }
}
