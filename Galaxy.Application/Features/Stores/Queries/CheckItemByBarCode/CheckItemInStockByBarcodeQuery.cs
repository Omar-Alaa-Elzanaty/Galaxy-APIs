using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.Stores.Queries.CheckItemByBarCode
{
    public record CheckItemInStockByBarcodeQuery : IRequest<Response>
    {
        public string BarCode { get; set; }

        public CheckItemInStockByBarcodeQuery(string barCode)
        {
            BarCode = barCode;
        }
    }

    internal class GetProductByBarCodeQueryHandler : IRequestHandler<CheckItemInStockByBarcodeQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<GetProductByBarCodeQueryHandler> _localization;
        private readonly IMapper _mapper;

        public GetProductByBarCodeQueryHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<GetProductByBarCodeQueryHandler> localization,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _localization = localization;
            _mapper = mapper;
        }

        public async Task<Response> Handle(CheckItemInStockByBarcodeQuery query, CancellationToken cancellationToken)
        {
            var entity = _unitOfWork.Repository<Stock>().Entities()
                        .SingleOrDefaultAsync(x => x.BarCode == query.BarCode).Result?.Product;

            if (entity is null)
            {
                return await Response.FailureAsync(_localization["InvalidRequest"].Value);
            }

            var product = entity.Adapt<CheckItemInStockByBarcodeQueryDto>(_mapper.Config);

            return await Response.SuccessAsync(product, _localization["Success"].Value);
        }
    }
}
