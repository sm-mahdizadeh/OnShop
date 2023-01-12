using AutoMapper;
using OnShop.Domain.Product.Commands.Brands;
using OnShop.Domain.Product.Commands.Categories;
using OnShop.Domain.Product.Dtos.Brands;
using OnShop.Domain.Product.Dtos.Categories;
using OnShop.Domain.Slider.Commands;
using OnShop.Domain.Slider.Dtos;

namespace OnShop.Web.Common
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<GetBrandDto, EditBrandDto>();
            CreateMap<EditBrandDto, BrandUpdateCommand>();

            CreateMap<GetCategoryDto, EditCategoryDto>();
            CreateMap<EditCategoryDto, CategoryUpdateCommand>();


            CreateMap<SliderDto, EditSliderDto>();
            CreateMap<EditSliderDto, UpdateSliderCommand>();

            CreateMap<SliderDto,OnShop.Domain.Slider.Entities.Slider>();

        }
    }
}