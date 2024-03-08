using Galaxy.Application.Interfaces.Repositories.Suppliers;
using Galaxy.Domain.Models;
using Galaxy.Presistance.Context;
using Microsoft.EntityFrameworkCore;
using Pharamcy.Presistance.Repositories;

namespace Galaxy.Presistance.Repositories
{
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        private readonly GalaxyDbContext _context;
        public SupplierRepository(GalaxyDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
