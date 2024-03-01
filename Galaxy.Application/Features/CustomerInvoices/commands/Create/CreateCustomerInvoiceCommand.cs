using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.CustomerInvoices.commands.Create
{
    public record CreateCustomerInvoiceCommand:IRequest<Response>
    {
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public List<OrderItems> Items { get; set; }
    }
    public record OrderItems
    {
        public string ProductName { get; set; }
        public double ItemPrice { get; set; }
        public double TotalPrice { get; set; }
        public List<long> BarCodes { get; set; }
    }
    internal class CreateCustomerInvoiceCommandHandler : IRequestHandler<CreateCustomerInvoiceCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<CreateCustomerInvoiceCommandHandler> _localization;
        public CreateCustomerInvoiceCommandHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<CreateCustomerInvoiceCommandHandler> localization)
        {
            _unitOfWork = unitOfWork;
            _localization = localization;
        }

        public async Task<Response> Handle(CreateCustomerInvoiceCommand command, CancellationToken cancellationToken)
        {

            var customer = await _unitOfWork.Repository<Customer>()
                        .GetItemOnAsync(x => x.Name == command.CustomerName && x.PhoneNumber == command.PhoneNumber);

            if(customer is null)
            {
                customer = new()
                {
                    Name = command.CustomerName,
                    PhoneNumber = command.PhoneNumber,
                };

                await _unitOfWork.Repository<Customer>().AddAsync(customer);
                _ = await _unitOfWork.SaveAsync();
            }

            var invoice = new List<CustomerInvoice>();
            var invoiceId = new Guid();

            foreach (var item in command.Items)
            {
                CustomerInvoice saleOrder = new()
                {
                    CustomerId = customer.Id,
                    ProductName = item.ProductName,
                    Quantity = item.BarCodes.Count,
                    InvoiceId = invoiceId,
                    ItemPrice = item.TotalPrice
                };

                invoice.Add(saleOrder);

                var itemsInStore = await _unitOfWork.Repository<Stock>().Entities()
                                .Where(x => item.BarCodes.Contains(x.BarCode)).ToListAsync();

                await _unitOfWork.Repository<Stock>().DeleteRange(itemsInStore);

            }

            await _unitOfWork.Repository<CustomerInvoice>().AddRangeAsync(invoice);
            _ = await _unitOfWork.SaveAsync();
            
            return await Response.SuccessAsync(_localization["Success"].Value);
        }
    }
}
