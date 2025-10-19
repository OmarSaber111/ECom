using AutoMapper;
using Ecom.Core.Dtos.Order;
using Ecom.Core.Entities.IdentityEntities;
using Ecom.Core.Entities.Order;

namespace Ecom.Api.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<ShippingAddress,ShippingAddressDto>().ReverseMap();
            CreateMap<Orders, OrderToReturnDto>()
    .ForMember(des => des.deliveryMethod,
        opt => opt.MapFrom(src => src.deliveryMethod != null ? src.deliveryMethod.Name : "No Delivery"))
    .ReverseMap();

            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Address,ShippingAddressDto>().ReverseMap();
        }
    }
}
