using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Application.Features.CustomerInvoices.Queries.GetAllCustomerInvoiceByCustomerId
{
    public class GetAllcustomerInvoiceByCustomerIdQueryDto
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public DateTime CreationDate { get; set; }
    }


}
