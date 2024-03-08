namespace Galaxy.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double PurchasePrice { get; set; }
        public double CurrentPurChase { get; set; }
        public double SellingPrice { get; set; }
        public double ProfitRatio { get; set; }
    }
}
