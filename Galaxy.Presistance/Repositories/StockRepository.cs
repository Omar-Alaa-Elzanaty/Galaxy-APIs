using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Presistance.Context;
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
    }
}
