using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Galaxy.Application.Features.Suppliers.Queries.GetAllLatestPruchases
{
    public record GetAllLatestSupplierPruchasesQuery : PaginatedRequest, IRequest<PaginatedResponse<GetAllLatestPruchasesQueryDto>>
    {
        public LatestPurchasesColumn LatestPurchasesColumn { get; set; }
    }

    internal class GetAllLatestPruchasesQueryHandler : IRequestHandler<GetAllLatestSupplierPruchasesQuery, PaginatedResponse<GetAllLatestPruchasesQueryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllLatestPruchasesQueryHandler(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResponse<GetAllLatestPruchasesQueryDto>> Handle(GetAllLatestSupplierPruchasesQuery query, CancellationToken cancellationToken)
        {

            var suppliersLastPruchases = _unitOfWork.Repository<Supplier>().Entities().Select(x => new
            {
                SupplierId = x.Id,
                x.Name,
                Details = x.Invoices.OrderBy(i => i.CreationDate).FirstOrDefault()
            });

            if (!query.KeyWord.IsNullOrEmpty())
            {
                suppliersLastPruchases = suppliersLastPruchases.Where(x => x.Name == query.KeyWord);
            }

            switch (query.LatestPurchasesColumn)
            {
                case LatestPurchasesColumn.Name:
                    suppliersLastPruchases = suppliersLastPruchases.OrderBy(x => x.Name);
                    break;

                case LatestPurchasesColumn.Date:
                    suppliersLastPruchases = suppliersLastPruchases.OrderBy(x => x.Details.CreationDate);
                    break;

                case LatestPurchasesColumn.TotalPay:
                    suppliersLastPruchases = suppliersLastPruchases.OrderBy(x => x.Details.TotalPay);
                    break;
            }


            suppliersLastPruchases = suppliersLastPruchases.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize);

            var suppliersCount = await _unitOfWork.Repository<Supplier>().Entities().CountAsync();

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

            return PaginatedResponse<GetAllLatestPruchasesQueryDto>
                .Create(latestPurchases, suppliersCount, query.PageNumber, query.PageSize);
        }
    }
}
