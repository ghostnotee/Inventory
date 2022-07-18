using FluentValidation;

namespace Inventory.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(i => i.Name).NotEmpty().NotNull();
        RuleFor(i => i.BrandId).NotEmpty().NotNull();
        RuleFor(i => i.CategoryId).NotEmpty().NotNull();
        RuleFor(i => i.Model).NotEmpty().NotNull();
        RuleFor(i => i.Company).NotEmpty().NotNull();
        RuleFor(i => i.SerialNumber).NotEmpty().NotNull();
    }
}