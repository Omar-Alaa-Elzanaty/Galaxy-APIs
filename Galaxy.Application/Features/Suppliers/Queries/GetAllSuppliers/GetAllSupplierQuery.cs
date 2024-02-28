using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Application.Interfaces.Repositories.Suppliers;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Galaxy.Application.Features.Suppliers.Queries.GetAllSuppliers
{
    public record GetAllSupplierQuery:IRequest<Response>;

    internal class GetAllSupplierQueryHandler : IRequestHandler<GetAllSupplierQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISupplierRepository _supplierRepository;

        public GetAllSupplierQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ISupplierRepository supplierRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _supplierRepository = supplierRepository;
        }

        public async Task<Response> Handle(GetAllSupplierQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.Repository<Supplier>().GetAllAsync();

            var supplierLastPurshaces = await _supplierRepository.GetSupplirsLatestPruchases();
            var suppliers = _mapper.Map<List<GetAllSupplierQueryDto>>(entities);

            foreach(var supplier in suppliers)
            {
                supplier.LatestPurchase = supplierLastPurshaces.FirstOrDefault(x => x.SupplierId == supplier.Id)?.LastPruchase;
            }

            return await Response.SuccessAsync(suppliers);
        }
    }
}
