using AutoMapper;
using Inventory.Application.Exceptions;
using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using Inventory.Identity.Jwt;
using MediatR;

namespace Inventory.Application.Features.Users.Commands.CreateRefreshToken;

public class CreateRefreshTokenCommand : IRequest<AccessTokenViewModel>
{
    public string RefreshToken { get; set; }
}

public class CreateRefreshTokenCommandHandler : IRequestHandler<CreateRefreshTokenCommand, AccessTokenViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly ITokenHelper _tokenHelper;
    private readonly IMapper _mapper;

    public CreateRefreshTokenCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper, IMapper mapper,
        IUserOperationClaimRepository userOperationClaimRepository, IOperationClaimRepository operationClaimRepository)
    {
        _userRepository = userRepository;
        _tokenHelper = tokenHelper;
        _mapper = mapper;
        _userOperationClaimRepository = userOperationClaimRepository;
        _operationClaimRepository = operationClaimRepository;
    }

    public async Task<AccessTokenViewModel> Handle(CreateRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var dbUser = await _userRepository.GetAsync(u =>
            u.RefreshToken == request.RefreshToken && u.RefreshTokenExpiration > DateTime.Now);
        if (dbUser is null) throw new NotFoundException("Not found a valid refreshtoken");

        var claims = from operationClaim in _operationClaimRepository.Get()
            join userOperationClaim in _userOperationClaimRepository.Get() on operationClaim.Id equals
                userOperationClaim.OperationClaimId
            where userOperationClaim.UserId == dbUser.Id
            select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };

        var jwt = _tokenHelper.CreateToken(dbUser, claims.ToList());
        dbUser.RefreshToken = jwt.RefreshToken;
        dbUser.RefreshTokenExpiration = jwt.RefreshTokenExpiration;
        
        await _userRepository.UpdateAsync(dbUser.Id, dbUser);
        
        var jwtViewModel = _mapper.Map<AccessTokenViewModel>(jwt);

        return jwtViewModel;
    }
}