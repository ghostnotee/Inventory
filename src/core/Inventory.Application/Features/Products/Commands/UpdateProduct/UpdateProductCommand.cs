using AutoMapper;
using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using MediatR;

namespace Inventory.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<ProductViewModel>
{
    public string Id { get; set; }
    public string CategoryId { get; set; }
    public string BrandId { get; set; }
    public string Model { get; set; }
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public Companies Company { get; set; }
    public string Description { get; set; }
    public DebitTicket DebitTicket { get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductViewModel>
{
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductRepository productRepository, IBrandRepository brandRepository,
        ICategoryRepository categoryRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ProductViewModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var dbProduct = await _productRepository.GetByIdAsync(request.Id);
        if (dbProduct is null) throw new InvalidOperationException($"{nameof(Product)} not found with id");

        var dbBrand = await _brandRepository.GetByIdAsync(request.BrandId);
        if (dbBrand is null) throw new InvalidOperationException($"{nameof(Brand)} not found with id");

        var dbCategory = await _categoryRepository.GetByIdAsync(request.CategoryId);
        //if (dbCategory is null) throw new NotFoundException(nameof(Category), request.CategoryId);

        var updatedProduct = new Product
        {
            Id = dbProduct.Id,
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

        await _productRepository.UpdateAsync(dbProduct.Id, updatedProduct);

        return _mapper.Map<ProductViewModel>(updatedProduct);
    }
}