using Galaxy.Application.Extention;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Galaxy.Application.Features.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery : PaginatedRequest, IRequest<PaginatedResponse<GetAllProductsQueryDto>>
    {
        public ProductColumnName ProductColumnName { get; set; }
        public string? KeyWord { get; set; }
    }
    internal class GetAllProductQueryHandler : IRequestHandler<GetAllProductsQuery, PaginatedResponse<GetAllProductsQueryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductQueryHandler(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResponse<GetAllProductsQueryDto>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            var entities = _unitOfWork.Repository<Product>().Entities().Select(x => new GetAllProductsQueryDto()
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                ProfitRatio = ((double)x.SellingPrice / x.CurrentPurchase) * 100
            });

            if (!query.KeyWord.IsNullOrEmpty())
            {
                entities = entities.Where(x => x.Name.Contains(query.KeyWord));
            }

            switch (query.ProductColumnName)
            {
                case ProductColumnName.Name:
                    entities = entities.OrderBy(x => x.Name);
                    break;

                case ProductColumnName.ProfitRatio:
                    entities = entities.OrderBy(x => x.ProfitRatio);
                    break;
            }

            var products = await entities.ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);

            return products;
        }
    }
}
