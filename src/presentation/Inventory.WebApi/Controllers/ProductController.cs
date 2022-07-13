using Inventory.Application.Features.Products.Commands.CreateProduct;
using Inventory.Application.Features.Products.Commands.DeleteProduct;
using Inventory.Application.Features.Products.Commands.UpdateProduct;
using Inventory.Application.Features.Products.Queries.GetAllProducts;
using Inventory.Application.Features.Products.Queries.GetProductById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebApi.Controllers
{
    public class ProductController : ApiController
    {
        [Authorize(Roles = "List")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllProductsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id) return BadRequest();
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            return Ok(await Mediator.Send(new DeleteProductCommand() { Id = id }));
        }
    }
}