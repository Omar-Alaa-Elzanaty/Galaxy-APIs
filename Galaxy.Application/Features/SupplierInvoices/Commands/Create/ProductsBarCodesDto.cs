namespace Galaxy.Application.Features.SupplierInvoices.Commands.Create
{
    public class ProductsBarCodesDto
    {
        public string ProductName { get; set; }
        public List<string> BarCodes { get; set; } = [];
    }
}
