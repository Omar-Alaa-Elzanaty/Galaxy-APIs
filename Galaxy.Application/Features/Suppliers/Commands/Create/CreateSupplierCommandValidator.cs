
using FluentValidation;

namespace Galaxy.Application.Features.Suppliers.Commands.Create
{
    public class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name required");
            RuleFor(x=>x.IdFile).NotEmpty().WithMessage("Id  required");
            RuleFor(x=>x.ImageFile).NotEmpty().WithMessage("Image required");
        }
    }
}
