using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Domain.Models
{
    public class Supplier : BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string IdUrl { get; set; }
        public virtual ICollection<SupplierInovice> SupplierInovices { get; set; }
    }
}
