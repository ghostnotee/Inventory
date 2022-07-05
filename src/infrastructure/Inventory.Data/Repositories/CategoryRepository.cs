using Inventory.Application.Interfaces.Repositories;
using Inventory.Data.Context;
using Inventory.Domain.Entities;

namespace Inventory.Data.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(IMongoDbContext context) : base(context)
    {
    }
}