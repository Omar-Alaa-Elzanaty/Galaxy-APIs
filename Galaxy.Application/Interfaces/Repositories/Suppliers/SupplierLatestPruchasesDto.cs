using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Application.Interfaces.Repositories.Suppliers
{
    public class SupplierLatestPruchasesDto
    {
        public int SupplierId { get; set; }
        public DateTime? LastPruchase { get; set; }
    }
}
