using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.Products.Queries.GetProductByBarCode
{
    public record GetProductByBarCodeQuery : IRequest<Response>
    {
        public string BarCode { get; set; }

        public GetProductByBarCodeQuery(string barCode)
        {
            BarCode = barCode;
        }
    }
    internal class GetItemByBarCodeQueryHandler : IRequestHandler<GetProductByBarCodeQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<GetItemByBarCodeQueryHandler> _localization;

        public GetItemByBarCodeQueryHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<GetItemByBarCodeQueryHandler> localization)
        {
            _unitOfWork = unitOfWork;
            _localization = localization;
        }

        public async Task<Response> Handle(GetProductByBarCodeQuery query, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Repository<Product>().Entities()
                .FirstOrDefaultAsync(x => x.ItemsInStock.Any(x => x.BarCode == query.BarCode));

            if (product is null)
            {
                return await Response.FailureAsync(_localization["ItemNotFound"].Value);
            }

            var itemDetails = new GetProductByBarCodeQueryDto()
            {
                ProductId = product.Id,
                ProductName = product.Name,
                SellingPrice = product.SellingPrice
            };

            return await Response.SuccessAsync(itemDetails, _localization["Success"].Value);
        }
    }
}
