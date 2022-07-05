using AutoMapper;
using Inventory.Application.Interfaces.Repositories;
using MediatR;

namespace Inventory.Application.Features.Queries.Brands.GetAllBrands;

public class GetAllBrandsQuery : IRequest<List<BrandViewModel>>
{
}

public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, List<BrandViewModel>>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public GetAllBrandsQueryHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<List<BrandViewModel>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brands = _brandRepository.Get().ToList();
        return await Task.FromResult(_mapper.Map<List<BrandViewModel>>(brands));
    }
}