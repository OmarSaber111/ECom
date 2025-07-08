using AutoMapper;
using Ecom.Core.Dtos.Photo;
using Ecom.Core.Dtos.Product;
using Ecom.Core.Entities.Product;

namespace Ecom.Api.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(des=>des.CategoryName,opt=>opt.MapFrom(src=>src.Category.Name));
            CreateMap<PhotoDto, Photo>().ReverseMap();

            CreateMap<AddProductDto,Product >()
                .ForMember(d => d.Photos, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<UpdateProductDto, Product>()
               .ForMember(d => d.Photos, opt => opt.Ignore())
               .ReverseMap();
        }
    }
}
