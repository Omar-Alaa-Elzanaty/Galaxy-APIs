using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.Products.Commands.Create
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        private readonly IStringLocalizer<AddProductCommandValidator> _localization;


        public AddProductCommandValidator(IStringLocalizer<AddProductCommandValidator> localization)
        {
            _localization = localization;
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image required");
            RuleFor(x => x.Rating).InclusiveBetween(1, 5).WithMessage(_localization["RatingLimitException"].Value);
        }
    }
}
