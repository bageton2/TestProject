using AutoMapper;
using Web.Models;

namespace Web
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<OrderDTO, Order>()
                .ForMember(order => order.Product, opt => opt.MapFrom(orderDTO => orderDTO.Product.Name))
                .ForMember(order => order.PhotoUrl, opt => opt.MapFrom(orderDTO => orderDTO.Product.PhotoUrl))
                .ForMember(order => order.Price, opt => opt.MapFrom(orderDTO => orderDTO.Product.Price))
                .ForMember(order => order.Stautus, opt => opt.MapFrom(orderDTO => orderDTO.Status.Name));

            CreateMap<Order, OrderDTO>()
                .ForMember(orderDTO => orderDTO.Product, opt => opt.MapFrom(order => new ProductDTO
                {
                    Name = order.Product,
                    Price = order.Price,
                    PhotoUrl = order.PhotoUrl
                }))
                .ForMember(orderDTO => orderDTO.Status, opt => opt.MapFrom(order => new StatusDTO
                {
                   Name = order.Stautus
                }));
        }
    }
}
