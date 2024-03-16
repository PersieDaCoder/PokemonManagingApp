using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Infrastructure.Data;
using PokemonManagingApp.Infrastructure.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PokemonManagingApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDBContext>();
        
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPokemonRepository, PokemonRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IPokemonOwnerRepository, PokemonOwnerRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IPokemonCategoryRepository, PokemonCategoryRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewerRepository, ReviewerRepository>();
        }

        return services;
    }
}