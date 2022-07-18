using FluentValidation;

namespace Inventory.Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(i => i.Name)
            .NotNull().WithMessage("{PropertyNme} must not be null or empty")
            .NotEmpty().WithMessage("{PropertyNme} must not be null or empty");
    }
}