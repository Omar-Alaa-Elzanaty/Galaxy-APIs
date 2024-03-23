using FluentValidation;
using Galaxy.Application.Extention;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using Mapster;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Galaxy.Application.Features.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery : PaginatedRequest, IRequest<Response>
    {
        public int? Evaluation { get; set; }
    }

    internal class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GetAllProductsQuery> _validator;

        public GetAllProductsQueryHandler(
            IUnitOfWork unitOfWork,
            IValidator<GetAllProductsQuery> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Response> Handle(GetAllProductsQuery command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            var entities = _unitOfWork.Repository<Product>().Entities();

            if (command.Evaluation is not null)
            {
                entities = entities.Where(x => x.Rating == command.Evaluation);
            }

            if (!command.KeyWord.IsNullOrEmpty())
            {
                entities = entities.Where(x => x.Name.ToLower().Contains(command.KeyWord));
            }

            entities = entities.OrderBy(x => x.Name);

            return await entities.ProjectToType<GetAllProductsQueryDto>()
                          .ToPaginatedListAsync(command.PageNumber, command.PageSize, cancellationToken);
        }
    }
}
