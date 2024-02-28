using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Core.Interfaces.Data.Repositories;
using DemoAPI.Core.Models;

namespace DemoAPI.Infrastructure.Data.Repository;

public class ReviewerRepository : BaseRepository<Reviewer>, IReviewerRepository
{
  public ReviewerRepository(ApplicationDBContext context) : base(context)
  {
  }
}