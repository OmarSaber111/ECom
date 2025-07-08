using AutoMapper;
using Ecom.Core.Dtos.Category;
using Ecom.Core.Entities.Product;

namespace Ecom.Api.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<AddCategoryDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
