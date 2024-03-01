using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Domain.Models
{
    public class CustomerInvoice : BaseEntity
    {
        public string ProductName { get; set; }
        public Guid InvoiceId { get; set; }
        public int Quantity { get; set; }
        public double ItemPrice { get; set; }
        public double Total { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
