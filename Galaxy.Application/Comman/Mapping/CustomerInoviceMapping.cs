using Galaxy.Application.Features.CustomerInvoices.Queries.GetAllCustomerInvoiceByCustomerId;
using Galaxy.Application.Features.CustomerInvoices.Queries.GetCustomerInvoiceById;
using Galaxy.Application.Features.Customers.Querires.GetCustomerById;
using Galaxy.Domain.Models;
using Mapster;

namespace Galaxy.Application.Comman.Mapping
{
    internal class CustomerInoviceMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CustomerInvoice, GetAllcustomerInvoiceByCustomerIdQuery>();
            config.NewConfig<CustomerInvoice, GetCustomerInvoiceByIdQuery>();
        }
    }
}
