using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryValidator : AbstractValidator<GetAllProductsQuery>
    {
        public GetAllProductsQueryValidator(IStringLocalizer<GetAllProductsQueryValidator> localization)
        {
            RuleFor(x => x.Evaluation).InclusiveBetween(1, 5).WithMessage(localization["RatingLimitException"].Value);
        }
    }
}
