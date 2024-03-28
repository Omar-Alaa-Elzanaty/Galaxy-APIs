using Galaxy.Application.Extention;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MediatR;

namespace Galaxy.Application.Features.Suppliers.Queries.GetAllSuppliers
{
    public record GetAllSupplierQuery : PaginatedRequest, IRequest<PaginatedResponse<GetAllSupplierQueryDto>>
    {
        public GetAllSupplierColumn? GetAllSupplierColumn { get; set; }
    }

    internal class GetAllSupplierQueryHandler : IRequestHandler<GetAllSupplierQuery, PaginatedResponse<GetAllSupplierQueryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSupplierQueryHandler(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResponse<GetAllSupplierQueryDto>> Handle(GetAllSupplierQuery query, CancellationToken cancellationToken)
        {
            var suppliers = _unitOfWork.Repository<Supplier>().Entities()
                           .Select(x => new GetAllSupplierQueryDto()
                           {
                               Id = x.Id,
                               Name = x.Name,
                               IdUrl = x.IdUrl,
                               ImageUrl = x.ImageUrl,
                               LatestPurchase = x.Invoices.OrderBy(x => x.CreationDate).FirstOrDefault()!.CreationDate
                           });


            if (query.KeyWord is not null)
            {
                suppliers = suppliers.Where(x => x.Name.ToLower().Contains(query.KeyWord));
            }

            if(query.GetAllSupplierColumn is not null)
            {
                switch (query.GetAllSupplierColumn)
                {
                    case GetAllSupplierColumn.Name:
                        suppliers = suppliers.OrderBy(x => x.Name);
                        break;

                    case GetAllSupplierColumn.LatestPurchase:
                        suppliers = suppliers.OrderByDescending(x => x.LatestPurchase);
                        break;
                }
            }

            return await suppliers.ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
        }
    }
}
