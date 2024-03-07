using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Domain.Models
{
    public class CustomerInvoiceItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double ItemPrice { get; set; }
        public double Total { get; set; }
        public int CustomerInvoiceId { get; set; }
        public virtual CustomerInvoice CustomerInvoice  { get; set; }
    }
}
