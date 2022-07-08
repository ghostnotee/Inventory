using AutoMapper;
using Inventory.Application.Features.Queries.Products;
using Inventory.Application.Interfaces.Repositories;
using MediatR;

namespace Inventory.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<ProductViewModel>
{
    public string Id { get; set; }
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ProductViewModel>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public DeleteProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductViewModel> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var deletedProduct = await _productRepository.DeleteAsync(request.Id);
        return _mapper.Map<ProductViewModel>(deletedProduct);
    }
}