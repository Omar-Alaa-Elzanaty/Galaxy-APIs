namespace Galaxy.Application.Features.Products.Queries.GetProductByBarCode
{
    public class GetProductByBarCodeQueryDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double SellingPrice { get; set; }
    }
}
