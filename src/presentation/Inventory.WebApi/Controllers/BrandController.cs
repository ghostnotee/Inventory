using Inventory.Application.Features.Brands.Commands.CreateBrand;
using Inventory.Application.Features.Brands.Commands.DeleteBrand;
using Inventory.Application.Features.Brands.Commands.UpdateBrand;
using Inventory.Application.Features.Brands.Queries.GetAllBrands;
using Inventory.Application.Features.Brands.Queries.GetBrandById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebApi.Controllers
{
    //[Authorize]
    public class BrandController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllBrandsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            return Ok(await Mediator.Send(new GetBrandByIdQuery { Id = id }));
        }

        //[Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateBrandCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //[Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            return Ok(await Mediator.Send(new DeleteBrandCommand() { Id = id }));
        }

        //[Authorize(Roles = "Manager")]
        [HttpPut]
        public async Task<ActionResult> Update(UpdateBrandCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}