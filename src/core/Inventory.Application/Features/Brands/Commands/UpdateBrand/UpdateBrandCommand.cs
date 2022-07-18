using AutoMapper;
using Inventory.Application.Exceptions;
using Inventory.Application.Interfaces.Repositories;
using MediatR;

namespace Inventory.Application.Features.Brands.Commands.UpdateBrand;

public class UpdateBrandCommand : IRequest<BrandViewModel>
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, BrandViewModel>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public UpdateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<BrandViewModel> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var dbBrand = await _brandRepository.GetByIdAsync(request.Id);
        if (dbBrand is null) throw new NotFoundException("Brand", request.Id);

        dbBrand.Name = request.Name;
        dbBrand.UpdateDate = DateTime.Now;
        await _brandRepository.UpdateAsync(request.Id, dbBrand);
        var viewModel = _mapper.Map<BrandViewModel>(dbBrand);

        return viewModel;
    }
}