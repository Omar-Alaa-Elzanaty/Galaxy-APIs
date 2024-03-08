using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MediatR;

namespace Galaxy.Application.Features.Products.Queries.GetProductsNames
{
    public record GetProductsNamesQuery:IRequest<Response>;

    internal class GetProductsNamesQueryHandler : IRequestHandler<GetProductsNamesQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductsNamesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(GetProductsNamesQuery query, CancellationToken cancellationToken)
        {
            var products = _unitOfWork.Repository<Product>().Entities().Select(x => new GetProductsNamesQueryDto()
            {
                Id = x.Id,
                Name = x.Name
            });

            return await Response.SuccessAsync(products);

        }
    }
}
