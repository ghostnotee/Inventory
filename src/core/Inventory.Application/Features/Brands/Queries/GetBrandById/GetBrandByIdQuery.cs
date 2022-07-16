using AutoMapper;
using Inventory.Application.Interfaces.Repositories;
using MediatR;

namespace Inventory.Application.Features.Brands.Queries.GetBrandById;

public class GetBrandByIdQuery : IRequest<BrandViewModel>
{
    public string Id { get; set; }
}

public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, BrandViewModel>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public GetBrandByIdQueryHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<BrandViewModel> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var dbBrand = await _brandRepository.GetByIdAsync(request.Id);
        if (dbBrand is null) throw new InvalidOperationException($"Brand not found with id");
        return _mapper.Map<BrandViewModel>(dbBrand);
    }
}