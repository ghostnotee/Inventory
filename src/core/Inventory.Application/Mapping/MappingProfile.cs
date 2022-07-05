using AutoMapper;
using Inventory.Application.Features.Queries.Brands;
using Inventory.Application.Features.Queries.Products;
using Inventory.Domain.Entities;

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
    }
}