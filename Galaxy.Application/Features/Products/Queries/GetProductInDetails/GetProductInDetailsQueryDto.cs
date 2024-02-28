namespace Galaxy.Application.Features.Products.Queries.GetProductInDetails
{
    public class GetProductInDetailsQueryDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string ProductName { get; set; }
        public int NumberInStock { get; set; }
        public int NumberInStore { get; set; }
        public int TransferOperations { get; set; }
        public int ProductTrack { get; set; }
        public double SellingPrice { get; set; }
        public double PruchasePrice { get; set; }
    }
}
