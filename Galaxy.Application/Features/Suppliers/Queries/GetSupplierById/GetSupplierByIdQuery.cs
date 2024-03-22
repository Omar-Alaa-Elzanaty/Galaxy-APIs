using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MediatR;
using Microsoft.Extensions.Localization;
using Pharamcy.Application.Interfaces.Media;

namespace Galaxy.Application.Features.Suppliers.Queries.GetSupplierById
{
    public record GetSupplierByIdQuery : IRequest<Response>
    {
        public int Id { get; set; }

        public GetSupplierByIdQuery(int id)
        {
            Id = id;
        }
    }

    internal class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSupplierByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(GetSupplierByIdQuery query, CancellationToken cancellationToken)
        {
            var supplier = _unitOfWork.Repository<Supplier>().Entities().Where(x => x.Id == query.Id)
                    .Select(x => new GetSupplierByIdQueryDto()
                    {
                        Id = x.Id,
                        IdUrl = x.IdUrl,
                        Name = x.Name,
                        ImageUrl = x.ImageUrl,
                        CreationDate = x.CreationDate,
                        LastPurchase = x.Invoices.OrderByDescending(x => x.CreationDate).FirstOrDefault()!.CreationDate
                    });

            return await Response.SuccessAsync(supplier);
        }
    }
}
