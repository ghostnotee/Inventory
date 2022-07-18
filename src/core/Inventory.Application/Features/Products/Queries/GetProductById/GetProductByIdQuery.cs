using AutoMapper;
using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQuery : IRequest<ProductViewModel>
{
    public string Id { get; set; }
}

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductViewModel>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IBrandRepository brandRepository,
        ICategoryRepository categoryRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<ProductViewModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var queryable = from product in _productRepository.Get(p => p.Id == request.Id)
            join brand in _brandRepository.Get() on product.BrandId equals brand.Id
            join category in _categoryRepository.Get() on product.CategoryId equals category.Id
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

        var result = queryable.FirstOrDefault();
        return await Task.FromResult(_mapper.Map<ProductViewModel>(result));
    }
}