using System.Data;
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
    public record CreateSupplierInvoice : IRequest<Response>
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
    internal class CreateSupplierInvoiceHandler : IRequestHandler<CreateSupplierInvoice, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBarCodeSerivce _barCodeSerivce;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateSupplierInvoiceHandler> _localization;
        private readonly IStockRepository _stockRepository;
        private readonly IConfiguration _config;

        public CreateSupplierInvoiceHandler(
            IUnitOfWork unitOfWork,
            IBarCodeSerivce barCodeSerivce,
            IMapper mapper,
            IStringLocalizer<CreateSupplierInvoiceHandler> localization,
            IConfiguration config,
            IStockRepository stockRepository)
        {
            _unitOfWork = unitOfWork;
            _barCodeSerivce = barCodeSerivce;
            _mapper = mapper;
            _localization = localization;
            _config = config;
            _stockRepository = stockRepository;
        }

        public async Task<Response> Handle(CreateSupplierInvoice command, CancellationToken cancellationToken)
        {
            var newInovice = new SupplierInvoice()
            {
                Items = new List<SupplierInvoiceItem>(),
                TotalPay = command.TotalInvoiceCost,
                SupplierId = command.SupplierId
            };

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

                //for (int i = 0; i < item.Quantity; i++)
                //{
                //    var itemInstock = new Stock()
                //    {
                //        ProductId = item.ProductId,
                //        BarCode = yearCode + product.SerialCode + _barCodeSerivce.CompleteString(serialNumber.ToString(), 5),
                //        IsInStock = true,
                //        SupplierId = command.SupplierId
                //    };
                //    //dataTable.Rows.Add(itemInstock.BarCode,itemInstock.IsInStock,itemInstock.ProductId,itemInstock.SupplierId);
                //    serialNumber++;
                //}

                product = _mapper.Map(item, product); //update product

                await _unitOfWork.Repository<Product>().UpdateAsync(product);


                newInovice.Items.Add(new SupplierInvoiceItem()
                {
                    ItemPrice = item.CurrentPurchase,
                    Quantity = item.Quantity,
                    ProductId = product.Id,
                    Total = item.TotalCost
                });
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

            return await Response.SuccessAsync(_localization["Success"].Value);
        }
    }
}
