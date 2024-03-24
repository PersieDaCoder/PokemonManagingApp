using Microsoft.Extensions.Caching.Memory;
using PokemonManagingApp.Core.Interfaces.Caching;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class UnitOfWork(ApplicationDBContext context, ICacheService cacheService) : IUnitOfWork
{
    private readonly ApplicationDBContext _context = context;
    private readonly ICacheService _cacheService = cacheService;
    private ICategoryRepository _categoryRepository = null!;
    private ICountryRepository _countryRepository = null!;
    private IOwnerRepository _ownerRepository = null!;
    private IPokemonCategoryRepository _pokemonCategoryRepository = null!;
    private IPokemonOwnerRepository _pokemonOwnerRepository = null!;
    private IPokemonRepository _pokemonRepository = null!;
    private IReviewRepository _reviewRepository = null!;
    private IGymRepository _gymRepository = null!;

    public ICategoryRepository CategoryRepository
    {
        get
        {
            if (_categoryRepository is null)
            {
                _categoryRepository = new CategoryRepository(_context, _cacheService);
            }
            return _categoryRepository;
        }
    }

    public ICountryRepository CountryRepository
    {
        get
        {
            if (_countryRepository is null)
            {
                _countryRepository = new CountryRepository(_context, _cacheService);
            }
            return _countryRepository;
        }
    }

    public IOwnerRepository OwnerRepository
    {
        get
        {
            if (_ownerRepository is null)
            {
                _ownerRepository = new OwnerRepository(_context, _cacheService);
            }
            return _ownerRepository;
        }
    }

    public IPokemonCategoryRepository PokemonCategoryRepository
    {
        get
        {
            if (_pokemonCategoryRepository is null)
            {
                _pokemonCategoryRepository = new PokemonCategoryRepository(_context, _cacheService);
            }
            return _pokemonCategoryRepository;
        }
    }

    public IPokemonOwnerRepository PokemonOwnerRepository
    {
        get
        {
            if (_pokemonOwnerRepository is null)
            {
                _pokemonOwnerRepository = new PokemonOwnerRepository(_context, _cacheService);
            }
            return _pokemonOwnerRepository;
        }
    }

    public IPokemonRepository PokemonRepository
    {
        get
        {
            if (_pokemonRepository is null)
            {
                _pokemonRepository = new PokemonRepository(_context, _cacheService);
            }
            return _pokemonRepository;
        }
    }

    public IReviewRepository ReviewRepository
    {
        get
        {
            if (_reviewRepository is null)
            {
                _reviewRepository = new ReviewRepository(_context, _cacheService);
            }
            return _reviewRepository;
        }
    }

    public IGymRepository GymRepository
    {
        get
        {
            if (_gymRepository is null)
            {
                _gymRepository = new GymRepository(_context, _cacheService);
            }
            return _gymRepository;
        }
    }

    public void Dispose()
    {
        Dispose(true);
    }

    public void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        };
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}