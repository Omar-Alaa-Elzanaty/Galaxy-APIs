using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Galaxy.Application.Features.SupplierInvoices.Commands.Create
{
    public class CreateSupplierInvoiceItemCommand:AbstractValidator<SupplierImportItem>
    {
        public CreateSupplierInvoiceItemCommand()
        {
            RuleFor(x => x.SellingPrice).GreaterThan(0);
            RuleFor(x=>x.CurrentPurchase).GreaterThan(0);
            RuleFor(x=>x.ProductId).GreaterThan(0);
            RuleFor(x=>x.Quantity).GreaterThan(0);
            RuleFor(x=>x.TotalCost).GreaterThan(0);
        }
    }
}
