using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MapsterMapper;
using MediatR;

namespace Galaxy.Application.Features.CustomerInvoices.Queries.GetAllCustomerInvoiceByCustomerId
{
    public record GetAllcustomerInvoiceByCustomerIdQuery : IRequest<Response>
    {
        public int Id { get; set; }

        public GetAllcustomerInvoiceByCustomerIdQuery(int id)
        {
            Id = id;
        }
    }
    public class GetAllcustomerInvoiceQueryHandler : IRequestHandler<GetAllcustomerInvoiceByCustomerIdQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllcustomerInvoiceQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetAllcustomerInvoiceByCustomerIdQuery query, CancellationToken cancellationToken)
        {
            var entities = _unitOfWork.Repository<CustomerInvoice>()
                           .GetOnCriteriaAsync(x => x.CustomerId == query.Id, x => x)
                           .Result.ToList();

            var customerInovices = _mapper.Map<List<GetAllcustomerInvoiceByCustomerIdQueryDto>>(entities);

            return await Response.SuccessAsync(customerInovices);
        }
    }
}
