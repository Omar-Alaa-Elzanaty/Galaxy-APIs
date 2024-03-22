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
        public List<OrderItems> Items { get; set; }
    }
    public record OrderItems
    {
        public List<string> BarCodes { get; set; }
    }
    internal class CreateCustomerInvoiceCommandHandler : IRequestHandler<CreateCustomerInvoiceCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<CreateCustomerInvoiceCommandHandler> _localization;
        private readonly IValidator<CreateCustomerInvoiceCommand> _validator;
        public CreateCustomerInvoiceCommandHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<CreateCustomerInvoiceCommandHandler> localization,
            IValidator<CreateCustomerInvoiceCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _localization = localization;
            _validator = validator;
        }

        public async Task<Response> Handle(CreateCustomerInvoiceCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }
            var barCodes = command.Items.SelectMany(x => x.BarCodes).ToList();

            var checkBarCodes = _unitOfWork.Repository<Stock>().Entities().Count(x => barCodes.Contains(x.BarCode));

            if (barCodes.Count != checkBarCodes)
            {
                return await Response.FailureAsync(_localization["ItemsNotFound"].Value);
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
                CustomerId = customer.Id
            };

            var ItemsInStore = new List<Stock>();

            foreach (var item in command.Items)
            {
                var product = _unitOfWork.Repository<Stock>().Entities()
                    .FirstAsync(x => x.BarCode == item.BarCodes.First(), cancellationToken: cancellationToken)
                    .Result.Product;

                invoice.Items.Add(new()
                {
                    ProductName = product.Name,
                    Quantity = item.BarCodes.Count,
                    ItemPrice = product.SellingPrice,
                    Total = item.BarCodes.Count * product.SellingPrice
                });

                ItemsInStore.AddRange(await _unitOfWork.Repository<Stock>().Entities()
                               .Where(x => item.BarCodes.Contains(x.BarCode)).ToListAsync());
            }

            await _unitOfWork.Repository<Stock>().DeleteRange(ItemsInStore);
            await _unitOfWork.Repository<CustomerInvoice>().AddAsync(invoice);
            _ = await _unitOfWork.SaveAsync();


            invoice.Total = invoice.Items.Sum(x => x.Total);

            return await Response.SuccessAsync(_localization["Success"].Value);
        }
    }
}
