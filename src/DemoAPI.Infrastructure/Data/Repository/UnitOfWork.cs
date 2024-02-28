using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Core.Interfaces.Data;
using DemoAPI.Core.Interfaces.Data.Repositories;

namespace DemoAPI.Infrastructure.Data.Repository;

public class UnitOfWork(ApplicationDBContext context) : IUnitOfWork
{
    private readonly ApplicationDBContext _context = context;
    private ICategoryRepository? _categoryRepository;
    private ICountryRepository? _countryRepository;

    private IOwnerRepository? _ownerRepository;
    private IPokemonCategoryRepository? _pokemonCategoryRepository;
    private IPokemonOwnerRepository? _pokemonOwnerRepository;
    private IPokemonRepository? _pokemonRepository;
    private IReviewRepository? _reviewRepository;
    private IReviewerRepository? _reviewerRepository;


    public ICategoryRepository CategoryRepository
    {
        get
        {
            if (_categoryRepository == null)
            {
                _categoryRepository = new CategoryRepository(_context);
            }
            return _categoryRepository;
        }
    }

    public ICountryRepository CountryRepository
    {
        get
        {
            if (_countryRepository == null)
            {
                _countryRepository = new CountryRepository(_context);
            }
            return _countryRepository;
        }
    }

    public IOwnerRepository OwnerRepository
    {
        get
        {
            if (_ownerRepository == null)
            {
                _ownerRepository = new OwnerRepository(_context);
            }
            return _ownerRepository;
        }
    }

    public IPokemonCategoryRepository PokemonCategoryRepository
    {
        get
        {
            if (_pokemonCategoryRepository == null)
            {
                _pokemonCategoryRepository = new PokemonCategoryRepository(_context);
            }
            return _pokemonCategoryRepository;
        }
    }

    public IPokemonOwnerRepository PokemonOwnerRepository
    {
        get
        {
            if (_pokemonOwnerRepository == null)
            {
                _pokemonOwnerRepository = new PokemonOwnerRepository(_context);
            }
            return _pokemonOwnerRepository;
        }
    }

    public IPokemonRepository PokemonRepository
    {
        get
        {
            if (_pokemonRepository == null)
            {
                _pokemonRepository = new PokemonRepository(_context);
            }
            return _pokemonRepository;
        }
    }

    public IReviewRepository ReviewRepository
    {
        get
        {
            if (_reviewRepository == null)
            {
                _reviewRepository = new ReviewRepository(_context);
            }
            return _reviewRepository;
        }
    }

    public IReviewerRepository ReviewerRepository
    {
        get
        {
            if (_reviewerRepository == null)
            {
                _reviewerRepository = new ReviewerRepository(_context);
            }
            return _reviewerRepository;
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