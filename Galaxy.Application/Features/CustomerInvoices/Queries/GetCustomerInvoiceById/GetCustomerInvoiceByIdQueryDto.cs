using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Application.Features.CustomerInvoices.Queries.GetCustomerInvoiceById
{
    public class GetCustomerInvoiceByIdQueryDto
    {
        public double Total { get; set; }
        public DateTime CreationDate { get; set; }
        public List<GetCustomerInvoiceByIdQueryItemDto> Items { get; set; }
    }
    public class GetCustomerInvoiceByIdQueryItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double ItemPrice { get; set; }
        public double Total { get; set; }
        public int CustomerInvoiceId { get; set; }
    }
}
