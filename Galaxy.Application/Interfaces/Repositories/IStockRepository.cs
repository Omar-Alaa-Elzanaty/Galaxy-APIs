using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Domain.Models;

namespace Galaxy.Application.Interfaces.Repositories
{
    public interface IStockRepository:IBaseRepository<Stock>
    {
        Task<int> NumberOfProductInStock(int productId);
        Task<int> NumberOfProductInStore(int productId);
        public void InsertImportToStock(int startSerial, int productId, int supplierId, int quantity, string intialCode);

    }
}
