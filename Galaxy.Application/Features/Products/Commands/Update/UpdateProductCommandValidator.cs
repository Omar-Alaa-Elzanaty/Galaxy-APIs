using FluentValidation;

namespace Galaxy.Application.Features.Products.Commands.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Rating).InclusiveBetween(1, 5);
            RuleFor(x => x.Name).MaximumLength(20);
        }
    }
}
