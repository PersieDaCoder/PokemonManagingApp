using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;

namespace PokemonManagingApp.Core.Interfaces.Data;

  public interface IUnitOfWork : IDisposable
  {
      ICategoryRepository CategoryRepository { get; }
      ICountryRepository CountryRepository { get; }
      IOwnerRepository OwnerRepository { get; }
      IPokemonCategoryRepository PokemonCategoryRepository { get; }
      IPokemonOwnerRepository PokemonOwnerRepository { get; }
      IPokemonRepository PokemonRepository { get; }
      IReviewRepository ReviewRepository { get; }
      IReviewerRepository ReviewerRepository { get; }
      Task SaveChangesAsync();
  }