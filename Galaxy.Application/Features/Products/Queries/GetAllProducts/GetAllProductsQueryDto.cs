namespace Galaxy.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double ProfitRatio { get; set; }
    }
    public enum ProductColumnName
    {
        Name = 1,
        ProfitRatio = 2
    }
}
