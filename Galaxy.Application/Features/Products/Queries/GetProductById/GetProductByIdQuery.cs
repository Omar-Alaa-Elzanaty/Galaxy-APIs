using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.Products.Queries.GetProductById
{
    public record GetProductByIdQuery : IRequest<Response>
    {
        public int Id { get; set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }

    internal class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetProductByIdQueryHandler> _localization;
        public GetProductByIdQueryHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<GetProductByIdQueryHandler> localization,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _localization = localization;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<Domain.Models.Product>().GetByIdAsync(query.Id);

            if (entity is null)
            {
                return await Response.FailureAsync(_localization["ItemNotFound"].Value);
            }

            var product = _mapper.Map<GetProductByIdQueryDto>(entity);

            return await Response.SuccessAsync(product, _localization["Success"].Value);
        }
    }
}
