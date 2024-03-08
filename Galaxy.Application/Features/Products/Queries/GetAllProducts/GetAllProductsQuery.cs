using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Shared;
using MapsterMapper;
using MediatR;

namespace Galaxy.Application.Features.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery:IRequest<Response>
    {

    }
    internal class GetAllProductQueryHandler : IRequestHandler<GetAllProductsQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllProductQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.Repository<Domain.Models.Product>().GetAllAsync();

            var products = _mapper.Map<List<GetAllProductsQueryDto>>(entities);

            return await Response.SuccessAsync(products);
        }
    }
}
