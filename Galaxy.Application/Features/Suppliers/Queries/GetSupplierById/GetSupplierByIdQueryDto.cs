using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Application.Features.Suppliers.Queries.GetSupplierById
{
    public class GetSupplierByIdQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? IdUrl { get; set; }
        public DateTime CreationDate {  get; set; }
        public DateTime? LastPurchase { get; set; }
    }
}
