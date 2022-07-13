using Inventory.Application.Interfaces.Repositories;
using Inventory.Data.Context;
using Inventory.Data.Repositories;
using Inventory.Data.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Inventory.Data;

public static class DependencyInjection
{
    public static void AddInfrastructureData(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IMongoDbSettings>(provider =>
            provider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

        serviceCollection.AddScoped<IMongoDbContext, MongoDbContext>();
        serviceCollection.AddScoped<IBrandRepository, BrandRepository>();
        serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
        serviceCollection.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
    }
}