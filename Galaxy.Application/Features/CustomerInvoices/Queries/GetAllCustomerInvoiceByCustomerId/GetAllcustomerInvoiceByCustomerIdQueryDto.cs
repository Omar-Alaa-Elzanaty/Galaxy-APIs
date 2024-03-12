namespace Galaxy.Application.Features.CustomerInvoices.Queries.GetAllCustomerInvoiceByCustomerId
{
    public class GetAllcustomerInvoiceByCustomerIdQueryDto
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public DateTime CreationDate { get; set; }
    }
    public enum GetAllCustomerInvoiceByCustomerIdColumn
    {
        Total = 1,
        CreationDate = 2
    }

}
