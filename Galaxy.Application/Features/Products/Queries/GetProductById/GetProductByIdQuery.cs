using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _memoryCache;
        public GetProductByIdQueryHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<GetProductByIdQueryHandler> localization,
            IMapper mapper,
            IMemoryCache cacheEntry)
        {
            _unitOfWork = unitOfWork;
            _localization = localization;
            _mapper = mapper;
            _memoryCache = cacheEntry;
        }

        public async Task<Response> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<Product>().GetByIdAsync(query.Id);

            if (entity is null)
            {
                return await Response.FailureAsync(_localization["ItemNotFound"].Value);
            }

            var product = _mapper.Map<GetProductByIdQueryDto>(entity);

            product.ProfitRatio = entity.SellingPrice - entity.CurrentPurchase;

            product.PurchasePrice = await _memoryCache.GetOrCreateAsync(
                "product" + product.Id.ToString(),
                async cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(10);

                    var productInStockDate = _unitOfWork.Repository<Stock>().Entities()
                    .Where(x => x.ProductId == entity.Id)
                    .OrderBy(x => x.CreationDate)
                    .FirstOrDefaultAsync().Result?.CreationDate;

                    if(productInStockDate is null)
                    {
                        return product.CurrentPurChase;
                    }

                    var lastPurchasePrices = await _unitOfWork.Repository<SupplierInvoiceItem>().Entities()
                        .Where(x => x.Product.Name == product.Name && x.SupplierInovice.CreationDate >= productInStockDate)
                        .Select(x => x.ItemPrice)
                        .ToListAsync();

                    return lastPurchasePrices.Sum() / lastPurchasePrices.Count;
                });

            //TODO: need to check

            return await Response.SuccessAsync(product, _localization["Success"].Value);
        }
    }
}
