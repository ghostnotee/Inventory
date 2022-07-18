using AutoMapper;
using Inventory.Application.Features.Brands;
using Inventory.Application.Features.Categories;
using Inventory.Application.Features.Products;
using Inventory.Application.Features.Users;
using Inventory.Domain.Entities;
using Inventory.Identity.Jwt;

namespace Inventory.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductViewModel>()
            .ForMember(x => x.Category, y =>
                y.MapFrom(z => z.Category.Name))
            .ForMember(x => x.Brand, y =>
                y.MapFrom(z => z.Brand.Name));

        CreateMap<Brand, BrandViewModel>();
        CreateMap<Category, CategoryViewModel>();
        CreateMap<User, UserViewModel>();
        CreateMap<User, LoginUserViewModel>();
        CreateMap<AccessToken, AccessTokenViewModel>();
    }
}