using AutoMapper;
using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using Inventory.Identity.Hashing;
using Inventory.Identity.Jwt;
using MediatR;

namespace Inventory.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<LoginUserViewModel>
{
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Department { get; set; }

    public List<string> OperationClaimIds { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, LoginUserViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly IMapper _mapper;
    private readonly ITokenHelper _tokenHelper;


    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper,
        IUserOperationClaimRepository userOperationClaimRepository, IOperationClaimRepository operationClaimRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _tokenHelper = tokenHelper;
        _userOperationClaimRepository = userOperationClaimRepository;
        _operationClaimRepository = operationClaimRepository;
    }

    public async Task<LoginUserViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        HashingHelper.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

        var createdUser = new User
        {
            Department = request.Department,
            EmailAddress = request.EmailAddress,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        var result = await _userRepository.AddAsync(createdUser);
        var userViewModel = _mapper.Map<LoginUserViewModel>(result);

        var claims = from operationClaim in _operationClaimRepository.Get()
            join userOperationClaim in _userOperationClaimRepository.Get() on operationClaim.Id equals
                userOperationClaim.OperationClaimId
            where userOperationClaim.UserId == result.Id
            select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };

        userViewModel.AccessToken = _tokenHelper.CreateToken(result,claims.ToList());
        
        return userViewModel;
    }
}