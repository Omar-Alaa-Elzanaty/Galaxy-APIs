using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _memoryCache;
        private readonly IStringLocalizer<GetCustomerByIdQueryHandler> _localization;

        public GetCustomerByIdQueryHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<GetCustomerByIdQueryHandler> stringLocalizer,
            IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;

            _localization = stringLocalizer;
            _memoryCache = memoryCache;
        }

        public async Task<Response> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<Customer>().GetByIdAsync(query.Id);

            if(entity is null)
            {
                return await Response.FailureAsync(_localization["InvalidRequest"].Value);
            }

            var customer = await _memoryCache.GetOrCreateAsync($"CustomerInvoiceHistory_{entity.Id}",async option =>
            {
                await Task.CompletedTask;
                option.SlidingExpiration = TimeSpan.FromMinutes(5);
                return entity.Adapt<GetCustomerByIdQueryDto>();
            });

            return await Response.SuccessAsync(customer!, _localization["Success"].Value);
        }
    }
}
