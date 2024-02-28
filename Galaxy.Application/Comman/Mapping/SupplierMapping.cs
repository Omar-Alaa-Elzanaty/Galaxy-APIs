using Galaxy.Application.Features.Suppliers.Commands.Create;
using Galaxy.Application.Features.Suppliers.Queries.GetAllLatestPruchases;
using Galaxy.Application.Features.Suppliers.Queries.GetAllSuppliers;
using Galaxy.Domain.Models;
using Mapster;

namespace Galaxy.Application.Comman.Mapping
{
    public class SupplierMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateSupplierCommand, Supplier>();
            config.NewConfig<Supplier, GetAllSupplierQueryDto>();
            config.NewConfig<Supplier, GetAllLatestPruchasesQueryDto>();
        }
    }
}
