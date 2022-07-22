using Inventory.Application.Features.Users.Commands.CreateRefreshToken;
using Inventory.Application.Features.Users.Commands.CreateUser;
using Inventory.Application.Features.Users.Commands.LoginUser;
using Inventory.Application.Features.Users.Queries.GetUserById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace Inventory.WebApi.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        
        
        [HttpPost("Google")]
        public async Task<IActionResult> Google(GoogleLoginUserCommand command)
        {
            var result = User.Identity.Name;
            return Ok(await Task.FromResult(result));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(CreateUserCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await Mediator.Send(new GetUserByIdQuery { Id = id });
            return Ok(result);
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult> RefreshToken(CreateRefreshTokenCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}