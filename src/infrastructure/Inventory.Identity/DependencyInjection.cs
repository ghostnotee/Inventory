using Inventory.Identity.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureIdentity(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ITokenHelper, JwtHelper>();
        return serviceCollection;
    }
}