using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Features.OperationClaims.Queries.GetAllOperationClaims;

public class GetAllOperationClaimsQuery : IRequest<List<OperationClaim>>
{
}

public class GetAllOperationClaimsQueryHandler : IRequestHandler<GetAllOperationClaimsQuery, List<OperationClaim>>
{
    private readonly IOperationClaimRepository _operationClaimRepository;

    public GetAllOperationClaimsQueryHandler(IOperationClaimRepository operationClaimRepository)
    {
        _operationClaimRepository = operationClaimRepository;
    }

    public async Task<List<OperationClaim>> Handle(GetAllOperationClaimsQuery request,
        CancellationToken cancellationToken)
    {
        var operationClaims = _operationClaimRepository.Get().ToList();

        return await Task.FromResult(operationClaims);
    }
}