using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> InsertBrand([FromBody] Brand brand)
        {
            return await _brandRepository.AddAsync(brand);
        }

        [HttpGet]
        public async Task<ActionResult<List<Brand>>> GetBrands()
        {
            var brands = _brandRepository.Get().ToList();
            return await Task.FromResult(brands);
        }
    }
}