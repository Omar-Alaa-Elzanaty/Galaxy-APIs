using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Galaxy.Application.Features.SupplierInvoices.Create
{
    public class CreateSupplierInvoiceCommandValidator:AbstractValidator<CreateSupplierInvoiceCommand>
    {
        public CreateSupplierInvoiceCommandValidator()
        {
            
        }
    }
}
