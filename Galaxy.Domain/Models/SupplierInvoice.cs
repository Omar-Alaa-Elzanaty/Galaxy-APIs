using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Galaxy.Domain.Models
{
    public class SupplierInvoice:BaseEntity
    {
        public double TotalPay { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get;set; }
        public virtual ICollection<SupplierInvoiceItem> Items { get; set; }
    }
}
