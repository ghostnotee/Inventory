using FluentValidation;
using Inventory.Application.Interfaces.Repositories;

namespace Inventory.Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    private readonly IBrandRepository _brandRepository;

    public CreateBrandCommandValidator(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
        RuleFor(i => i.Name)
            .NotEmpty().WithMessage("{PropertyName} cannot be null or empty")
            .NotNull().WithMessage("{PropertyName} cannot be null or empty")
            .Must(UniqName).WithMessage("This brand {PropertyName} already exists.");
    }

    private bool UniqName(string name)
    {
        var dbBrand = _brandRepository.GetAsync(b => b.Name.ToLower() == name.ToLower()).Result;
        return dbBrand == null;
    }
}