using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PokemonManagingApp.UseCases;

public static class DependancyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependancyInjection).GetTypeInfo().Assembly));
        return services;
    }
}