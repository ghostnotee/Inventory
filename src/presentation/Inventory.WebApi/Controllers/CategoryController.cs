using Inventory.Application.Features.Categories.Commands.CreateCategory;
using Inventory.Application.Features.Categories.Commands.DeleteCategory;
using Inventory.Application.Features.Categories.Commands.UpdateCategory;
using Inventory.Application.Features.Categories.Queries.GetCategoryById;
using Inventory.Application.Features.Queries.Categories.GetAllCategories;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebApi.Controllers
{
    public class CategoryController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllCategoriesQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            return Ok(await Mediator.Send(new GetCategoryByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCategoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            return Ok(await Mediator.Send(new DeleteCategoryCommand() { Id = id }));
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateCategoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}