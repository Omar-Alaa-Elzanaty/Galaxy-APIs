using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Application.Features.Suppliers.Queries.GetAllSuppliers
{
    public class GetAllSupplierQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdUrl { get; set;}
        public string ImageUrl { get; set;}
        public DateTime? LatestPurchase { get; set; }
    }
    public enum GetAllSupplierColumn
    {
        Name=1,
        LatestPurchase=2
    }
}
