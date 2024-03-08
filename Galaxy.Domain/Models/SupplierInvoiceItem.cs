using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Domain.Models
{
    public class SupplierInvoiceItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double ItemPrice { get; set; }
        public double Total { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int SupplierInvoiceId { get; set; }
        public virtual SupplierInvoice SupplierInovice { get; set; }
    }
}
