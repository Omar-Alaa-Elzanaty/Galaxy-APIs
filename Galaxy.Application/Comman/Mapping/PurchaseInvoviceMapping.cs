using Galaxy.Application.Features.Customers.Querires.GetCustomerById;
using Galaxy.Application.Features.Stores.Queries.CheckItemByBarCode;
using Galaxy.Application.Features.SupplierInvoices.Queries.GetPurchaseInvoiceById;
using Galaxy.Domain.Models;
using Mapster;

namespace Galaxy.Application.Comman.Mapping
{
    public class PurchaseInvoviceMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<SupplierInvoice, GetPurchaseInvoiceByIdDto>()
                .Map(dest => dest.SupplierName, src => src.Supplier.Name);
            config.NewConfig<SupplierInvoiceItem, GetPurchaseInvoiceByIdItemDto>()
                .Map(dest => dest.ProductName, src => src.Product.Name);
        }
    }
}
