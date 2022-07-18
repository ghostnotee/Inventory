using FluentValidation;

namespace Inventory.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(i => i.Id).NotEmpty().NotNull();
    }
}