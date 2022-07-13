using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Features.OperationClaims.Commands.CreateOperationClaim;

public class CreateOperationClaimCommand : IRequest<string>
{
    public string Name { get; set; }
}

public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, string>
{
    private readonly IOperationClaimRepository _operationClaimRepository;

    public CreateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository)
    {
        _operationClaimRepository = operationClaimRepository;
    }

    public async Task<string> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
    {
        var operationClaim = new OperationClaim { Name = request.Name };
        await _operationClaimRepository.AddAsync(operationClaim);
        return operationClaim.Id;
    }
}