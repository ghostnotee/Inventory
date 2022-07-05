using Inventory.Application.Interfaces.Repositories;
using Inventory.Data.Context;
using Inventory.Domain.Entities;

namespace Inventory.Data.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(IMongoDbContext context) : base(context)
    {
    }
}