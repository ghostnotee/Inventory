using AutoMapper;
using Inventory.Application.Exceptions;
using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<UserDetailViewModel>
{
    public string Id { get; set; }
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDetailViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper,
        IOperationClaimRepository operationClaimRepository, IUserOperationClaimRepository userOperationClaimRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _operationClaimRepository = operationClaimRepository;
        _userOperationClaimRepository = userOperationClaimRepository;
    }

    public async Task<UserDetailViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var dbUser = await _userRepository.GetByIdAsync(request.Id);
        if (dbUser is null) throw new NotFoundException("user", nameof(User));
        
        var claims = from operationClaim in _operationClaimRepository.Get()
            join userOperationClaim in _userOperationClaimRepository.Get() on operationClaim.Id equals
                userOperationClaim.OperationClaimId
            where userOperationClaim.UserId == dbUser.Id
            select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };

        var userDetail = new UserDetailViewModel()
        {
            Id = dbUser.Id,
            FirstName = dbUser.FirstName,
            LastName = dbUser.LastName,
            Department = dbUser.Department,
            EmailAddress = dbUser.EmailAddress,
            OperationClaims = claims.ToList()
        };

        return userDetail;
    }
}