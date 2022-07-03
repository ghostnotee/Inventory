using Inventory.Application.Interfaces.Repositories;
using Inventory.Data.Context;
using Inventory.Domain.Entities;

namespace Inventory.Data.Repositories;

public class BrandRepository: GenericRepository<Brand>, IBrandRepository
{
    public BrandRepository(IMongoDbContext context) : base(context)
    {
    }
}