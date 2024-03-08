using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Application.Features.SupplierInvoices.Create
{
    public class ProductsBarCodesDto
    {
        public string ProductName { get; set; }
        public List<string> BarCodes { get; set; } = [];
    }
}
