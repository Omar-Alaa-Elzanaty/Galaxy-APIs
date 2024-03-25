using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.Products.Queries.GetProductInDetails
{
    public record GetProductInDetailsQuery : IRequest<Response>
    {
        public int Id { get; set; }

        public GetProductInDetailsQuery(int id)
        {
            Id = id;
        }
    }
    internal class GetProductInDetailsQueryHandler : IRequestHandler<GetProductInDetailsQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<GetProductInDetailsQueryHandler> _localization;

        public GetProductInDetailsQueryHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<GetProductInDetailsQueryHandler> localization)
        {
            _unitOfWork = unitOfWork;
            _localization = localization;
        }
        public async Task<Response> Handle(GetProductInDetailsQuery query, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(query.Id);

            if (product is null)
            {
                return await Response.FailureAsync(_localization["InvalidRequst"].Value);
            }

            var productInDetails = new GetProductInDetailsQueryDto()
            {
                Id = product.Id,
                NumberInStock = product.ItemsInStock.Where(x => x.IsInStock == true).Count(),
                NumberInStore = product.ItemsInStock.Where(x => x.IsInStock == false).Count(),
                ImageUrl = product.ImageUrl,
                ProductName = product.Name,
                ProductTrack = 0,
                LowInventoryIn = product.LowInventoryIn,
                Rating = product.Rating,
                PruchasePrice = product.CurrentPurchase,
                SellingPrice = product.SellingPrice,
                TransferOperations = 0
            };

            return await Response.SuccessAsync(productInDetails);
        }
    }
}
