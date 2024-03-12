namespace Galaxy.Application.Features.Suppliers.Queries.GetAllLatestPruchases
{
    public class GetAllLatestPruchasesQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? TotalPay { get; set; }
        public DateTime? Date { get; set; }
    }
    public enum LatestPurchasesColumn
    {
        Name = 1,
        TotalPay = 2,
        Date = 3,
    }
}
