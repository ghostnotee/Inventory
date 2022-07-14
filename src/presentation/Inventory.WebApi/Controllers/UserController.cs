using Inventory.Application.Features.Users.Commands.CreateRefreshToken;
using Inventory.Application.Features.Users.Commands.CreateUser;
using Inventory.Application.Features.Users.Commands.LoginUser;
using Inventory.Application.Features.Users.Queries.GetUserById;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("Register")]
        public async Task<IActionResult> Login(CreateUserCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
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