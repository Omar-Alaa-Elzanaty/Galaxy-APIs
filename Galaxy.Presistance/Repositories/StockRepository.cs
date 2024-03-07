using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Presistance.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pharamcy.Presistance.Repositories;

namespace Galaxy.Presistance.Repositories
{
    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
        private readonly GalaxyDbContext _context;
        public StockRepository(GalaxyDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<int> NumberOfProductInStock(int productId)
        {
            return await _context.ItemInStock.Where(x => x.ProductId == productId && x.IsInStock == true).CountAsync();
        }

        public async Task<int> NumberOfProductInStore(int productId)
        {
            return await _context.ItemInStock.Where(x => x.ProductId == productId && x.IsInStock == false).CountAsync();
        }

        public async void InsertImportToStock(int startSerial, int productId, int supplierId, int quantity, string intialCode)
        {
            _ = _context.Database
                .ExecuteSql($"EXEC Galaxy.GenerateBarCode {startSerial}, {productId}, {supplierId}, {quantity}, {intialCode}");
            await _context.SaveChangesAsync();
        }

    }
}
