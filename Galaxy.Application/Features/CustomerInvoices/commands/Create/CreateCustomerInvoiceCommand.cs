using FluentValidation;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.CustomerInvoices.commands.Create
{
    public record CreateCustomerInvoiceCommand : IRequest<Response>
    {
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public double TotalPrice { get; set; }
        public List<OrderItems> Items { get; set; }
    }
    public record OrderItems
    {
        public string ProductName { get; set; }
        public double ItemPrice { get; set; }
        public double TotalPrice { get; set; }
        public List<string> BarCodes { get; set; }
    }
    internal class CreateCustomerInvoiceCommandHandler : IRequestHandler<CreateCustomerInvoiceCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<CreateCustomerInvoiceCommandHandler> _localization;
        private readonly IValidator<CreateCustomerInvoiceCommand> _validator;
        public CreateCustomerInvoiceCommandHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<CreateCustomerInvoiceCommandHandler> localization)
        {
            _unitOfWork = unitOfWork;
            _localization = localization;
        }

        public async Task<Response> Handle(CreateCustomerInvoiceCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if(!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            var customer = await _unitOfWork.Repository<Customer>()
                        .GetItemOnAsync(x => x.PhoneNumber == command.PhoneNumber);

            if (customer is null)
            {
                customer = new()
                {
                    Name = command.CustomerName,
                    PhoneNumber = command.PhoneNumber,
                };

                await _unitOfWork.Repository<Customer>().AddAsync(customer);
                _ = await _unitOfWork.SaveAsync();
            }

            var invoice = new CustomerInvoice()
            {
                Items = new List<CustomerInvoiceItem>(),
                CustomerId = customer.Id,
                Total = command.TotalPrice
            };

            var ItemsInStore = new List<Stock>();

            foreach (var item in command.Items)
            {
                invoice.Items.Add(new()
                {
                    ProductName = item.ProductName,
                    Quantity = item.BarCodes.Count,
                    ItemPrice = item.TotalPrice
                });

                ItemsInStore.AddRange(await _unitOfWork.Repository<Stock>().Entities()
                               .Where(x => item.BarCodes.Contains(x.BarCode)).ToListAsync());
            }

            await _unitOfWork.Repository<Stock>().DeleteRange(ItemsInStore);
            await _unitOfWork.Repository<CustomerInvoice>().AddAsync(invoice);
            _ = await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(_localization["Success"].Value);
        }
    }
}
