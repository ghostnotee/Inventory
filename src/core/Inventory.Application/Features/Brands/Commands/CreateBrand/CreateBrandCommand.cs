using AutoMapper;
using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommand : IRequest<BrandViewModel>
{
    public string Name { get; set; }
}

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BrandViewModel>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<BrandViewModel> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = new Brand { Name = request.Name };
        await _brandRepository.AddAsync(brand);
        return _mapper.Map<BrandViewModel>(brand);
    }
}