using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Application.Features.Suppliers.Queries.GetAllSuppliers;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Application.Interfaces.Repositories.Suppliers;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MapsterMapper;
using MediatR;

namespace Galaxy.Application.Features.Suppliers.Queries.GetAllLatestPruchases
{
    public record GetAllLatestPruchasesQuery:IRequest<Response>;

    internal class GetAllLatestPruchasesQueryHandler : IRequestHandler<GetAllLatestPruchasesQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISupplierRepository _supplierRepository;

        public GetAllLatestPruchasesQueryHandler(
            IMapper mapper,
            ISupplierRepository supplierRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(GetAllLatestPruchasesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.Repository<Supplier>().GetAllAsync();
            var suppliers = _mapper.Map<List<GetAllLatestPruchasesQueryDto>>(entities);

            var latestPurchases = await _supplierRepository.GetSupplirsLatestPruchases();


            foreach (var supplier in suppliers)
            {
                supplier.LastPruchases = latestPurchases.SingleOrDefault(x=>x.SupplierId==supplier.Id)?.LastPruchase;
            }

            return await Response.SuccessAsync(latestPurchases);
        }
    }
}
