using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Core.Interfaces.Data;
using DemoAPI.Core.Interfaces.Data.Repositories;
using DemoAPI.Infrastructure.Data;
using DemoAPI.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoAPI.Infrastructure;

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