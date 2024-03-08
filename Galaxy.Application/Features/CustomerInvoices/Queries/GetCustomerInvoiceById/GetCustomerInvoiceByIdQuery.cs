using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.CustomerInvoices.Queries.GetCustomerInvoiceById
{
    public record GetCustomerInvoiceByIdQuery:IRequest<Response>
    {
        public int Id { get; set; }

        public GetCustomerInvoiceByIdQuery(int id)
        {
            Id = id;
        }
    }
    internal class GetCustomerInvoiceByIdQueryHandler : IRequestHandler<GetCustomerInvoiceByIdQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetCustomerInvoiceByIdQueryHandler> _localization;

        public GetCustomerInvoiceByIdQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<GetCustomerInvoiceByIdQueryHandler> localization)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localization = localization;
        }

        public async Task<Response> Handle(GetCustomerInvoiceByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<CustomerInvoice>().GetByIdAsync(query.Id);

            if(entity == null)
            {
                return await Response.FailureAsync(_localization["ItemNotFound"].Value);
            }

            var customerInvoice = _mapper.Map<GetCustomerInvoiceByIdQueryDto>(entity);

            return await Response.SuccessAsync(customerInvoice, _localization["Success"].Value);
        }
    }
}
