using Galaxy.Application.Features.Products.Commands.Create;
using Galaxy.Application.Features.Products.Commands.Update;
using Galaxy.Application.Features.Products.Queries.GetAllProductsCards;
using Galaxy.Application.Features.Products.Queries.GetProductById;
using Galaxy.Application.Features.Stores.Queries.CheckItemByBarCode;
using Galaxy.Application.Features.SupplierInvoices.Commands.Create;
using Galaxy.Domain.Models;
using Mapster;

namespace Galaxy.Application.Comman.Mapping
{
    public class ProductMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddProductCommand, Product>();
            config.NewConfig<Product, GetProductByIdQueryDto>();
            config.NewConfig<Product, GetAllProductsCardsQueryDto>();
            config.NewConfig<UpdateProductCommand, Product>();
            config.NewConfig<CreateSupplierInvoiceHandler, Product>();
            config.NewConfig<Product, CheckItemInStockByBarcodeQueryDto>();
        }
    }
}
