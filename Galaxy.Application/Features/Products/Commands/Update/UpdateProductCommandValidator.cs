using FluentValidation;

namespace Galaxy.Application.Features.Products.Commands.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Rating).InclusiveBetween(1, 5).WithMessage("RatingLimitException");
            RuleFor(x => x.Name).MaximumLength(20).WithMessage("Name must be less than or equal 20 character");
        }
    }
}
