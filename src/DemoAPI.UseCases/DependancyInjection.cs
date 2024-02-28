using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

using MediatR; // Add missing package reference

namespace DemoAPI.UseCases;

  public static class DependancyInjection
  {
      public static IServiceCollection AddUseCases(this IServiceCollection services)
      {
          services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependancyInjection).GetTypeInfo().Assembly));
          return services;
      }
  }