using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Infrastructure.Data;
using PokemonManagingApp.Infrastructure.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokemonManagingApp.Core.Interfaces.Caching;
using PokemonManagingApp.Infrastructure.Data.Caching;
using Microsoft.EntityFrameworkCore;

namespace PokemonManagingApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add DBContext
        services.AddDbContext<ApplicationDBContext>();
        // Add IMemoryCache
        services.AddMemoryCache();

        // Add other dependencies
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IPokemonRepository, PokemonRepository>();
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IPokemonOwnerRepository, PokemonOwnerRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IPokemonCategoryRepository, PokemonCategoryRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();

        // return services
        return services;
    }
}
