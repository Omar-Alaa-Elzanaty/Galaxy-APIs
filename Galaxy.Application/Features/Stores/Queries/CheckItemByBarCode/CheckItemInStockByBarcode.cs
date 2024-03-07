using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.Stores.Queries.CheckItemByBarCode
{
    public record CheckItemInStockByBarcode : IRequest<Response>
    {
        public string BarCode { get; set; }

        public CheckItemInStockByBarcode(string barCode)
        {
            BarCode = barCode;
        }
    }

    internal class GetProductByBarCodeQueryHandler : IRequestHandler<CheckItemInStockByBarcode, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<GetProductByBarCodeQueryHandler> _localization;

        public GetProductByBarCodeQueryHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<GetProductByBarCodeQueryHandler> localization)
        {
            _unitOfWork = unitOfWork;
            _localization = localization;
        }

        public async Task<Response> Handle(CheckItemInStockByBarcode query, CancellationToken cancellationToken)
        {
            var productIsFound = await _unitOfWork.Repository<Stock>().Entities()
                        .AnyAsync(x => x.BarCode == query.BarCode);

            if (!productIsFound)
            {
                return await Response.FailureAsync(_localization["InvalidRequest"].Value);
            }

            return await Response.SuccessAsync(_localization["Success"].Value);
        }
    }
}
