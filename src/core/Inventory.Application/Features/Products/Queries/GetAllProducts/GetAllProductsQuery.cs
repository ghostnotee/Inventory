using AutoMapper;
using Inventory.Application.Features.Queries.Products;
using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQuery : IRequest<List<ProductViewModel>>
{
}

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductViewModel>>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper,
        ICategoryRepository categoryRepository, IBrandRepository brandRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductViewModel>> Handle(GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        var queryable = from product in _productRepository.Get()
            join brand in _brandRepository.Get() on product.BrandId equals brand.Id
            join category in _categoryRepository.Get() on product.CategoryId equals category.Id
            orderby product.Company
            select new Product
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                Category = category,
                BrandId = product.BrandId,
                Brand = brand,
                Company = product.Company,
                Description = product.Description,
                Model = product.Model,
                Name = product.Name,
                DebitTicket = product.DebitTicket,
                SerialNumber = product.SerialNumber
            };

        var viewModel = _mapper.Map<List<ProductViewModel>>(queryable);
        return await Task.FromResult(new List<ProductViewModel>(viewModel));
    }
}