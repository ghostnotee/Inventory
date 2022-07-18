using FluentValidation;
using Inventory.Application.Interfaces.Repositories;

namespace Inventory.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    public CreateCategoryCommandValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
        RuleFor(i => i.Name)
            .NotEmpty().WithMessage("{PropertyName} cannot be null or empty")
            .NotNull().WithMessage("{PropertyName} cannot be null or empty")
            .Must(UniqName).WithMessage("This brand {PropertyName} already exists.");
    }

    private bool UniqName(string name)
    {
        var dbBrand = _categoryRepository.GetAsync(b => b.Name.ToLower() == name.ToLower()).Result;
        if (dbBrand == null) return true;

        return false;
    }
}