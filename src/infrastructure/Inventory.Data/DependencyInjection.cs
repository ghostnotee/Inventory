using Inventory.Application.Interfaces.Repositories;
using Inventory.Data.Context;
using Inventory.Data.Repositories;
using Inventory.Data.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Inventory.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureData(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddSingleton<IMongoDbSettings>(provider =>
            provider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

        serviceCollection.AddScoped<IMongoDbContext, MongoDbContext>();
        serviceCollection.AddTransient<IBrandRepository, BrandRepository>();

        return serviceCollection;
    }
}