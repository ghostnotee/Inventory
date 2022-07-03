using Inventory.Data.Settings;
using MongoDB.Driver;

namespace Inventory.Data.Context;

public class MongoDbContext : IMongoDbContext
{
    private IMongoDatabase MongoDatabase { get; }

    public MongoDbContext(IMongoDbSettings settings)
    {
        IMongoClient mongoClient = new MongoClient(settings.ConnectionString);
        MongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);

        InventoryDbSeed.SeedAsync(settings);
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return MongoDatabase.GetCollection<T>(name);
    }
}