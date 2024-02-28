using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Core.Interfaces.Data.Repositories;
using DemoAPI.Core.Models;

namespace DemoAPI.Infrastructure.Data.Repository;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDBContext context) : base(context)
    {
    }
}