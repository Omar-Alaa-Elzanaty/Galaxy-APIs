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
        public int Quantity { get; set; }
        public int ItemPrice { get; set; }
        public int Total { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
