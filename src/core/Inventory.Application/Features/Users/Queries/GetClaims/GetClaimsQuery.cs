using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Features.Users.Queries.GetClaims;

public class GetClaimsQuery : IRequest<List<OperationClaim>>
{
    public string UserId { get; set; }
}

public class GetClaimsQueryHandler : IRequestHandler<GetClaimsQuery, List<OperationClaim>>
{
    private readonly IUserRepository _userRepository;
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public GetClaimsQueryHandler(IUserRepository userRepository, IOperationClaimRepository operationClaimRepository,
        IUserOperationClaimRepository userOperationClaimRepository)
    {
        _userRepository = userRepository;
        _operationClaimRepository = operationClaimRepository;
        _userOperationClaimRepository = userOperationClaimRepository;
    }

    public async Task<List<OperationClaim>> Handle(GetClaimsQuery request, CancellationToken cancellationToken)
    {
        var queryable = from operationClaim in _operationClaimRepository.Get()
            join userOperationClaim in _userOperationClaimRepository.Get() on operationClaim.Id equals
                userOperationClaim.OperationClaimId
            where userOperationClaim.UserId == request.UserId
            select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };

        return await Task.FromResult(queryable.ToList());
    }
}