using Inventory.Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Inventory.Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Inventory.Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using Inventory.Application.Features.OperationClaims.Queries.GetAllOperationClaims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebApi.Controllers
{
    [Authorize]
    public class OperationClaimController : ApiController
    {
        
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllOperationClaimsQuery()));
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateOperationClaimCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [Authorize(Roles = "Manager")]
        [HttpPut]
        public async Task<ActionResult> Update(UpdateOperationClaimCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            return Ok(await Mediator.Send(new DeleteOperationClaimCommand() { Id = id }));
        }
    }
}