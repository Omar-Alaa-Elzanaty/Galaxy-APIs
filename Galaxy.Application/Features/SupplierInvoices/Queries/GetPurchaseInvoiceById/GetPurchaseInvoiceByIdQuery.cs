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
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.SupplierInvoices.Queries.GetPurchaseInvoiceById
{
    public record GetPurchaseInvoiceByIdQuery:IRequest<Response>
    {
        public int Id { get; set; }

        public GetPurchaseInvoiceByIdQuery(int id)
        {
            Id = id;
        }
    }

    internal class GetPurchaseInvoiceByIdQueryHandler : IRequestHandler<GetPurchaseInvoiceByIdQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<GetPurchaseInvoiceByIdQueryHandler> _localization;
        private readonly IMapper _mapper;

        public GetPurchaseInvoiceByIdQueryHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<GetPurchaseInvoiceByIdQueryHandler> localization,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _localization = localization;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetPurchaseInvoiceByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<SupplierInvoice>().GetByIdAsync(query.Id);

            if(entity is null)
            {
                return await Response.FailureAsync(_localization["NoInvoiceFound"].Value);
            }

            var supplier=_mapper.Map<GetPurchaseInvoiceByIdDto>(entity);

            return await Response.SuccessAsync(supplier);
        }
    }
}
