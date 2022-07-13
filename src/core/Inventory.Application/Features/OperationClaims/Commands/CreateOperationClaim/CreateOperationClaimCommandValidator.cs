using FluentValidation;

namespace Inventory.Application.Features.OperationClaims.Commands.CreateOperationClaim;

public class CreateOperationClaimCommandValidator : AbstractValidator<CreateOperationClaimCommand>
{
    public CreateOperationClaimCommandValidator()
    {
        RuleFor(i => i.Name).NotEmpty().NotNull().WithMessage("{PropertyName} cannot be null or empty");
    }
}