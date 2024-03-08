using FluentValidation;

namespace Galaxy.Application.Features.Products.Commands.Create
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
