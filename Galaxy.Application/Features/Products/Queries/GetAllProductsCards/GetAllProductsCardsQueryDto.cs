namespace Galaxy.Application.Features.Products.Queries.GetAllProductsCards
{
    public class GetAllProductsCardsQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double CurrentPurchase { get; set; }
        public double SellingPrice { get; set; }
        public int Rating { get; set; }
        public double ProfitRatio { get; set; }
        public int NumberInStock { get; set; }
        public int NumberInStore { get; set; }
    }
    public enum ProductColumnName
    {
        Name = 1,
        ProfitRatio = 2
    }
}
