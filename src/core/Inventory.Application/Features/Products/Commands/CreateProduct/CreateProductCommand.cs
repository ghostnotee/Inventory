using AutoMapper;
using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using MediatR;

namespace Inventory.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<ProductViewModel>
{
    public string CategoryId { get; set; }
    public string BrandId { get; set; }
    public string Model { get; set; }
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public Companies Company { get; set; }
    public string Description { get; set; }
    public DebitTicket DebitTicket { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductViewModel>
{
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper,
        IBrandRepository brandRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _brandRepository = brandRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ProductViewModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var dbBrand = await _brandRepository.GetByIdAsync(request.BrandId);
        if (dbBrand is null) throw new InvalidOperationException($"{nameof(Product)} not found");

        var dbCategory = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (dbCategory is null) throw new InvalidOperationException($"{nameof(Category)} not found");

        var savedProduct = new Product
        {
            BrandId = dbBrand.Id,
            Brand = dbBrand,
            CategoryId = dbCategory.Id,
            Category = dbCategory,
            Company = request.Company,
            Model = request.Model,
            DebitTicket = request.DebitTicket,
            Description = request.Description,
            Name = request.Name,
            SerialNumber = request.SerialNumber
        };

        await _productRepository.AddAsync(savedProduct);

        return _mapper.Map<ProductViewModel>(savedProduct);
    }
}