using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Domain.Models
{
    public class Stock:BaseEntity
    {
        public long BarCode { get; set; }
        public int ProductId { get; set; }
        public bool IsInStock {  get; set; }
        public virtual Product Product { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
