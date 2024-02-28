using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Domain.Models
{
    public class SupplierInovice:BaseEntity
    {
        public int SupplierId { get; set; }
        public int TotalPay { get; set; }
        public virtual Supplier Supplier { get;set; }
    }
}
