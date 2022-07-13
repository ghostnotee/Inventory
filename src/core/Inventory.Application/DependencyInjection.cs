using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreApplication(this IServiceCollection serviceCollection)
    {
        var assembly = Assembly.GetExecutingAssembly();

        serviceCollection.AddAutoMapper(assembly);
        serviceCollection.AddMediatR(assembly);
        serviceCollection.AddValidatorsFromAssembly(assembly);

        return serviceCollection;
    }
}