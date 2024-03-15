using FluentValidation;

namespace Galaxy.Application.Features.SupplierInvoices.Create
{
    public class CreateSupplierInvoiceCommandValidator : AbstractValidator<CreateSupplierInvoiceCommand>
    {
        public CreateSupplierInvoiceCommandValidator()
        {
            RuleFor(x => x.ImportItems).NotEmpty();
            RuleFor(x => x.SupplierId).NotEmpty();
            RuleFor(x => x.TotalInvoiceCost).GreaterThan(0);
        }
    }
}
