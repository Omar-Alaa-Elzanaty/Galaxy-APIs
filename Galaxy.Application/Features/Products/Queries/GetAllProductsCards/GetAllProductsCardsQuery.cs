using Galaxy.Application.Extention;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Galaxy.Application.Features.Products.Queries.GetAllProductsCards
{
    public record GetAllProductsCardsQuery : PaginatedRequest, IRequest<PaginatedResponse<GetAllProductsCardsQueryDto>>
    {
        public ProductColumnName? ProductColumnName { get; set; }
        public string? KeyWord { get; set; }
    }
    internal class GetAllProductQueryHandler : IRequestHandler<GetAllProductsCardsQuery, PaginatedResponse<GetAllProductsCardsQueryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductQueryHandler(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResponse<GetAllProductsCardsQueryDto>> Handle(GetAllProductsCardsQuery query, CancellationToken cancellationToken)
        {
            var entities = _unitOfWork.Repository<Product>().Entities().Select(x => new GetAllProductsCardsQueryDto()
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                ProfitRatio = ((double)x.SellingPrice / x.CurrentPurchase) * 100,
                NumberInStock = x.ItemsInStock.Where(s => s.IsInStock == true).Count(),
                NumberInStore = x.ItemsInStock.Where(s => s.IsInStock == false).Count()
            });

            if (!query.KeyWord.IsNullOrEmpty())
            {
                entities = entities.Where(x => x.Name.ToLower().Contains(query.KeyWord!.ToLower()!));
            }

            if (query.ProductColumnName is not null)
            {
                switch (query.ProductColumnName)
                {
                    case ProductColumnName.Name:
                        entities = entities.OrderBy(x => x.Name);
                        break;

                    case ProductColumnName.ProfitRatio:
                        entities = entities.OrderBy(x => x.ProfitRatio);
                        break;
                }
            }

            var products = await entities.ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);

            return products;
        }
    }
}
