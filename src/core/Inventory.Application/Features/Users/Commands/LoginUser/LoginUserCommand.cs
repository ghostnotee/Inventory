using AutoMapper;
using Inventory.Application.Exceptions;
using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using Inventory.Identity.Hashing;
using Inventory.Identity.Jwt;
using MediatR;

namespace Inventory.Application.Features.Users.Commands.LoginUser;

public class LoginUserCommand : IRequest<LoginUserViewModel>
{
    public string EmailAddress { get; set; }
    public string Password { get; set; }
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly ITokenHelper _tokenHelper;
    private readonly IMapper _mapper;


    public LoginUserCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper, IMapper mapper,
        IUserOperationClaimRepository userOperationClaimRepository, IOperationClaimRepository operationClaimRepository)
    {
        _userRepository = userRepository;
        _tokenHelper = tokenHelper;
        _mapper = mapper;
        _userOperationClaimRepository = userOperationClaimRepository;
        _operationClaimRepository = operationClaimRepository;
    }

    public async Task<LoginUserViewModel> Handle(LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        var userToCheck = await _userRepository.GetAsync(u => u.EmailAddress == request.EmailAddress);
        if (userToCheck is null) throw new NotFoundException("User not found");

        if (!HashingHelper.VerifyPasswordHash(request.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            throw new NotFoundException("Email or password is wrong");

        // TODO if (!userToCheck.EmailConfirmed) throw new NotFoundException("Email address is not confirmed yet");

        var claims = from operationClaim in _operationClaimRepository.Get()
            join userOperationClaim in _userOperationClaimRepository.Get() on operationClaim.Id equals
                userOperationClaim.OperationClaimId
            where userOperationClaim.UserId == userToCheck.Id
            select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };

        var claimList = claims.ToList();
        var userViewModel = _mapper.Map<LoginUserViewModel>(userToCheck);
        userViewModel.AccessToken = _tokenHelper.CreateToken(userToCheck, claimList);

        return userViewModel;
    }
}