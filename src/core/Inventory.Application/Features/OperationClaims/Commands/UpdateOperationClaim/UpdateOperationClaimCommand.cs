using Inventory.Application.Interfaces.Repositories;
using MediatR;

namespace Inventory.Application.Features.OperationClaims.Commands.UpdateOperationClaim;

public class UpdateOperationClaimCommand : IRequest<string>
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, string>
{
    private readonly IOperationClaimRepository _operationClaimRepository;

    public UpdateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository)
    {
        _operationClaimRepository = operationClaimRepository;
    }

    public async Task<string> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
    {
        var operationClaimToUpdate = await _operationClaimRepository.GetByIdAsync(request.Id);
        operationClaimToUpdate.UpdateDate = DateTime.Now;
        operationClaimToUpdate.Name = request.Name;
        var result = await _operationClaimRepository.UpdateAsync(request.Id, operationClaimToUpdate);

        return request.Id;
    }
}