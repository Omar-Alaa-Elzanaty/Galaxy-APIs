using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Domain.Models;

namespace Galaxy.Application.Features.SupplierInvoices.Queries.GetPurchaseInvoiceById
{
    public class GetPurchaseInvoiceByIdDto
    {
        public int Id { get; set; }
        public double TotalPay { get; set; }
        public string SupplierName { get; set; }
        public List<GetPurchaseInvoiceByIdItemDto> Items { get; set; } = [];
    }

    public class GetPurchaseInvoiceByIdItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double ItemPrice { get; set; }
        public double Total { get; set; }
        public int SupplierInvoiceId { get; set; }
    }
}
