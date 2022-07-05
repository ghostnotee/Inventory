using Inventory.Application.Features.Queries.Brands.GetAllBrands;
using Inventory.Application.Features.Queries.Brands.GetBrandById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetAllBrandsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            return Ok(await _mediator.Send(new GetBrandByIdQuery { Id = id }));
        }
    }
}