using FluentValidation;

namespace Galaxy.Application.Features.SupplierInvoices.Commands.Create
{
    public class CreateSupplierInvoiceCommandValidator : AbstractValidator<CreateSupplierInvoiceCommand>
    {
        public CreateSupplierInvoiceCommandValidator()
        {
            RuleFor(x => x.ImportItems).NotEmpty().WithMessage("Invoice must has items");
            RuleFor(x => x.SupplierId).NotEmpty().WithMessage("SupplierId is required");
            RuleFor(x => x.TotalInvoiceCost).GreaterThan(0).WithMessage("Total cost must be greater than 0");
        }
    }
}
