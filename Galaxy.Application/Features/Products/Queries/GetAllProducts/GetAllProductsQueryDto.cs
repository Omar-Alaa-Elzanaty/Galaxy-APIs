namespace Galaxy.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double CurrentPurchase { get; set; }
        public double SellingPrice { get; set; }
        public string ImageUrl { get; set; }
        public int Rating { get; set; }
        public int LowInventoryIn { get; set; }
    }
}
