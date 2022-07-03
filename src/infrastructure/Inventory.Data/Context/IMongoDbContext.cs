using MongoDB.Driver;

namespace Inventory.Data.Context;

public interface IMongoDbContext
{
    IMongoCollection<TEntity> GetCollection<TEntity>(string name);
}