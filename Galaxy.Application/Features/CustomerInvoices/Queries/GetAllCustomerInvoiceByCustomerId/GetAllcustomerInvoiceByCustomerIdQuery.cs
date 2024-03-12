using Galaxy.Application.Extention;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using Mapster;
using MediatR;
using SBS.Recruitment.Shared;

namespace Galaxy.Application.Features.CustomerInvoices.Queries.GetAllCustomerInvoiceByCustomerId
{
    public record GetAllcustomerInvoiceByCustomerIdQuery : PaginatedRequest, IRequest<PaginatedResponse<GetAllcustomerInvoiceByCustomerIdQueryDto>>
    {
        public int Id { get; set; }

        public GetAllCustomerInvoiceByCustomerIdColumn GetAllCustomerInvoiceByCustomerIdColumn { get; set; }
    }

    internal class GetAllcustomerInvoiceQueryHandler : IRequestHandler<GetAllcustomerInvoiceByCustomerIdQuery, PaginatedResponse<GetAllcustomerInvoiceByCustomerIdQueryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllcustomerInvoiceQueryHandler(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResponse<GetAllcustomerInvoiceByCustomerIdQueryDto>> Handle(GetAllcustomerInvoiceByCustomerIdQuery query, CancellationToken cancellationToken)
        {
            var customerInvoices = _unitOfWork.Repository<CustomerInvoice>()
                           .Entities().Where(x => x.CustomerId == query.Id);

            switch (query.GetAllCustomerInvoiceByCustomerIdColumn)
            {
                case GetAllCustomerInvoiceByCustomerIdColumn.Total:
                    customerInvoices = customerInvoices.OrderBy(x => x.Total);
                    break;

                case GetAllCustomerInvoiceByCustomerIdColumn.CreationDate:
                    customerInvoices = customerInvoices.OrderBy(x => x.CreationDate);
                    break;
            }

            return await customerInvoices.ProjectToType<GetAllcustomerInvoiceByCustomerIdQueryDto>()
                         .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);

        }

    }
}
