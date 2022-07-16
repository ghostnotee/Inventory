using AutoMapper;
using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
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
        if (dbBrand is null) throw new InvalidOperationException($"{nameof(Brand)} not found");

        dbBrand.Name = request.Name;
        dbBrand.UpdateDate = DateTime.Now;
        await _brandRepository.UpdateAsync(request.Id, dbBrand);
        var viewModel = _mapper.Map<BrandViewModel>(dbBrand);

        return viewModel;
    }
}