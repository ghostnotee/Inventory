using Inventory.Application.Exceptions;
using Inventory.Application.Interfaces.Repositories;
using MediatR;

namespace Inventory.Application.Features.OperationClaims.Commands.DeleteOperationClaim;

public class DeleteOperationClaimCommand : IRequest<string>
{
    public string Id { get; set; }
}

public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, string>
{
    private readonly IOperationClaimRepository _operationClaimRepository;

    public DeleteOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository)
    {
        _operationClaimRepository = operationClaimRepository;
    }

    public async Task<string> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
    {
        var operationClaimToDeleted = await _operationClaimRepository.DeleteAsync(request.Id);
        if (operationClaimToDeleted is null) throw new NotFoundException("Brand", request.Id);

        return operationClaimToDeleted.Id;
    }
}