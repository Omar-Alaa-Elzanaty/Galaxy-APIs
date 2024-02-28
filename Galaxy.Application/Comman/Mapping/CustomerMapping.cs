using Galaxy.Application.Features.Customers.Querires.GetAllCustomers;
using Galaxy.Application.Features.Customers.Querires.GetCustomerById;
using Galaxy.Domain.Models;
using Mapster;

namespace Galaxy.Application.Comman.Mapping
{
    public class CustomerMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Customer, GetAllCustomersQueryDto>();
            config.NewConfig<Customer, GetCustomerByIdQueryDto>()
                .Map(x => x.Invoices, y => y.Invoices);
        }
    }
}
