using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.Customers.Querires.GetCustomerById
{
    public record GetCustomerByIdQuery:IRequest<Response>
    {
        public int Id { get; set; }

        public GetCustomerByIdQuery(int id)
        {
            Id = id;
        }
    }
    internal class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetCustomerByIdQueryHandler> _localization;

        public GetCustomerByIdQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<GetCustomerByIdQueryHandler> stringLocalizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localization = stringLocalizer;
        }

        public async Task<Response> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<Customer>().GetByIdAsync(query.Id);

            if(entity is null)
            {
                return await Response.FailureAsync(_localization["InvalidRequest"].Value);
            }

            var customer = _mapper.Map<GetCustomerByIdQueryDto>(entity);

            return await Response.SuccessAsync(customer, _localization["Success"].Value);
        }
    }
}
