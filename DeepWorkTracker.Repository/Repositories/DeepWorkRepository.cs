using DeepWorkTracker.Core.Contracts.Repositories;
using DeepWorkTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepWorkTracker.Repository.Repositories
{
    public class DeepWorkRepository : GenericRepository<DeepWorkSession>, IDeepWorkRepository
    {
        ApplicationDbContext _context = null!;

        public DeepWorkRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
