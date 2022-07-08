using AutoMapper;
using Inventory.Application.Interfaces.Repositories;
using MediatR;

namespace Inventory.Application.Features.Brands.Commands.DeleteBrand;

public class DeleteBrandCommand : IRequest<BrandViewModel>
{
    public string Id { get; set; }
}

public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, BrandViewModel>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public DeleteBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<BrandViewModel> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var deletedBrand = await _brandRepository.DeleteAsync(request.Id);
        return _mapper.Map<BrandViewModel>(deletedBrand);
    }
}