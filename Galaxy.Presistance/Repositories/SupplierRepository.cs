using Galaxy.Application.Interfaces.Repositories.Suppliers;
using Galaxy.Domain.Models;
using Galaxy.Presistance.Context;
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

        public async Task<HashSet<SupplierLatestPruchasesDto>> GetSupplirsLatestPruchases()
        {
            await Task.CompletedTask;

            return _context.SuppliersInovices.GroupBy(x => x.SupplierId)
                                    .Select(x => new SupplierLatestPruchasesDto()
                                    {
                                        SupplierId = x.Key,
                                        LastPruchase = x.OrderByDescending(x => x.CreationDate).FirstOrDefault()!.CreationDate
                                    })
                                    .ToHashSet();
        }
    }
}
