using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Application.Interfaces.Repositories.Suppliers;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Galaxy.Application.Features.Suppliers.Queries.GetAllLatestPruchases
{
    public record GetAllLatestSupplierPruchasesQuery : IRequest<Response>;

    internal class GetAllLatestPruchasesQueryHandler : IRequestHandler<GetAllLatestSupplierPruchasesQuery, Response>
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

        public async Task<Response> Handle(GetAllLatestSupplierPruchasesQuery request, CancellationToken cancellationToken)
        {

            var suppliersLastPruchases = await _unitOfWork.Repository<Supplier>().Entities().Select(x => new
            {
                SupplierId = x.Id,
                x.Name,
                Details = x.Invoices.OrderBy(i => i.CreationDate).FirstOrDefault()
            }).ToListAsync();

            var latestPurchases = new List<GetAllLatestPruchasesQueryDto>();

            foreach (var purchase in suppliersLastPruchases)
            {
                latestPurchases.Add(new()
                {
                    Id = purchase.SupplierId,
                    Name = purchase.Name,
                    Date = purchase.Details?.CreationDate,
                    TotalPay = purchase.Details?.TotalPay
                });
            }

            return await Response.SuccessAsync(latestPurchases);
        }
    }
}
