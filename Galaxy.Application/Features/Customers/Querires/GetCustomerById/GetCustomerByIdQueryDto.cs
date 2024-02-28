namespace Galaxy.Application.Features.Customers.Querires.GetCustomerById
{
    public class GetCustomerByIdQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public List<GetCustomerByIdCustomerInvoiceDto> Invoices { get; set; }
    }
    public class GetCustomerByIdCustomerInvoiceDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int ItemPrice { get; set; }
    }
}
