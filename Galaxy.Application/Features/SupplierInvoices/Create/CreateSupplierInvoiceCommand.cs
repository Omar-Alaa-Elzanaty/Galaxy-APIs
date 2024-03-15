using System.Data;
using FluentValidation;
using Galaxy.Application.Interfaces.BarCode;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.SupplierInvoices.Create
{
    public record CreateSupplierInvoiceCommand : IRequest<Response>
    {
        public int SupplierId { get; set; }
        public double TotalInvoiceCost { get; set; }
        public HashSet<SupplierImportItem> ImportItems { get; set; }
    }
    public record SupplierImportItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double TotalCost { get; set; }
        public double CurrentPurchase { get; set; }
        public double SellingPrice { get; set; }
    }
    internal class CreateSupplierInvoiceHandler : IRequestHandler<CreateSupplierInvoiceCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBarCodeSerivce _barCodeSerivce;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateSupplierInvoiceHandler> _localization;
        private readonly IValidator<CreateSupplierInvoiceCommand> _validator;
        private readonly IStockRepository _stockRepository;
        private readonly IConfiguration _config;

        public CreateSupplierInvoiceHandler(
            IUnitOfWork unitOfWork,
            IBarCodeSerivce barCodeSerivce,
            IMapper mapper,
            IStringLocalizer<CreateSupplierInvoiceHandler> localization,
            IConfiguration config,
            IStockRepository stockRepository,
            IValidator<CreateSupplierInvoiceCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _barCodeSerivce = barCodeSerivce;
            _mapper = mapper;
            _localization = localization;
            _config = config;
            _stockRepository = stockRepository;
            _validator = validator;
        }

        public async Task<Response> Handle(CreateSupplierInvoiceCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            { 
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }
            
            foreach(var item in command.ImportItems)
            {
                if (item.CurrentPurchase == 0 || item.SellingPrice == 0)
                {
                    return await Response.FailureAsync(_localization["PriceNotBeZero"].Value);
                }
            }

            var newInovice = new SupplierInvoice()
            {
                Items = new List<SupplierInvoiceItem>(),
                TotalPay = command.TotalInvoiceCost,
                SupplierId = command.SupplierId
            };
            var BarCodes = new List<ProductsBarCodesDto>();

            var yearCode = _barCodeSerivce.CompleteString((DateTime.Now.Year % 1000).ToString(), 4);

            //DataTable dataTable = new("Galaxy.ItemInStock");
            //dataTable.Columns.Add("BarCode", typeof(string));
            //dataTable.Columns.Add("IsInStock", typeof(bool));
            //dataTable.Columns.Add("ProductId", typeof(int));
            //dataTable.Columns.Add("SupplierId", typeof(int));

            foreach (var item in command.ImportItems)
            {

                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.ProductId);

                if (product is null)
                {
                    return await Response.FailureAsync(_localization["ItemNotFound"].Value);
                }

                //Add items to stock
                var lastserial = _unitOfWork.Repository<Stock>().Entities()
                    .Where(x => x.BarCode.Substring(4, 4) == product.SerialCode)
                    .OrderByDescending(x => x.BarCode.Substring(8))
                    .FirstOrDefaultAsync().Result?.BarCode[8..];

                int serialNumber = 0;

                if (lastserial is not null)
                {
                    serialNumber = int.Parse(lastserial) + 1;
                }

                _stockRepository.InsertImportToStock
                (
                startSerial: serialNumber,
                productId: item.ProductId,
                supplierId: command.SupplierId,
                quantity: item.Quantity,
                intialCode: yearCode + product.SerialCode
                );

                var intialCode = yearCode + product.SerialCode;
                var productBarCodes = new ProductsBarCodesDto() { ProductName = product.Name };

                for (int i = 0; i < item.Quantity; i++)
                {
                    productBarCodes.BarCodes.Add(intialCode + _barCodeSerivce.CompleteString(serialNumber.ToString(), 5));

                    //var itemInstock = new Stock()
                    //{
                    //    ProductId = item.ProductId,
                    //    BarCode = yearCode + product.SerialCode + _barCodeSerivce.CompleteString(serialNumber.ToString(), 5),
                    //    IsInStock = true,
                    //    SupplierId = command.SupplierId
                    //};
                    //dataTable.Rows.Add(itemInstock.BarCode,itemInstock.IsInStock,itemInstock.ProductId,itemInstock.SupplierId);
                    serialNumber++;
                }

                product = _mapper.Map(item, product); //update product

                await _unitOfWork.Repository<Product>().UpdateAsync(product);


                newInovice.Items.Add(new SupplierInvoiceItem()
                {
                    ItemPrice = item.CurrentPurchase,
                    Quantity = item.Quantity,
                    ProductId = product.Id,
                    Total = item.TotalCost
                });

                BarCodes.Add(productBarCodes);
            }

            await _unitOfWork.Repository<SupplierInvoice>().AddAsync(newInovice);
            _ = await _unitOfWork.SaveAsync();

            //using (var connection = new SqlConnection(_config.GetConnectionString("DbConnection")))
            //{
            //    SqlBulkCopy bulkCopy = new SqlBulkCopy(
            //     connection,
            //     SqlBulkCopyOptions.TableLock |
            //     SqlBulkCopyOptions.FireTriggers |
            //     SqlBulkCopyOptions.UseInternalTransaction,
            //     null
            //     );
            //    bulkCopy.DestinationTableName = "Galaxy.ItemInStock";
            //    connection.Open();
            //    await bulkCopy.WriteToServerAsync(dataTable);
            //    connection.Close();
            //}

            return await Response.SuccessAsync(BarCodes,_localization["Success"].Value);
        }
    }
}
