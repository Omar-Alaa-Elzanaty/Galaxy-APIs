using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Galaxy.Application.Features.CustomerInvoices.commands.Create
{
    public class CreateCustomerInvoiceCommandValidator:AbstractValidator<CreateCustomerInvoiceCommand>
    {
        public CreateCustomerInvoiceCommandValidator()
        {
            RuleFor(x => x.PhoneNumber).MaximumLength(15);
            RuleFor(x => x.CustomerName).MaximumLength(40);
        }
    }
}
