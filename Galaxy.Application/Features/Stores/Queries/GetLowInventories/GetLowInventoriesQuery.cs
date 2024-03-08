using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MediatR;

namespace Galaxy.Application.Features.Stores.Queries.GetLowInventories
{
    public record GetLowInventoriesQuery:IRequest<Response>;

    internal class GetLowInventoriesQueryHandler : IRequestHandler<GetLowInventoriesQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLowInventoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(GetLowInventoriesQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Repository<Product>().GetAllAsync();

            var needToImport = new List<GetLowInventoriesQueryDto>();

            foreach(var product in products)
            {
                var currentAmount = product.ItemsInStock.Count;
                if (product.LowInventoryIn > currentAmount)
                {
                    needToImport.Add(new()
                    {
                        CurrentAmount = currentAmount,
                        LowLimit = product.LowInventoryIn,
                        ProdcutId = product.Id
                    });
                }
            }

            return await Response.SuccessAsync(needToImport);

        }
    }
}
