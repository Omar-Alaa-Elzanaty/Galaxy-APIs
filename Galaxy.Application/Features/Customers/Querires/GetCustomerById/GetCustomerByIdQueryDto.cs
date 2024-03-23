namespace Galaxy.Application.Features.Customers.Querires.GetCustomerById
{
    public class GetCustomerByIdQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<GetCustomerByIdCustomerInvoiceDto> Invoices { get; set; }
    }
    public class GetCustomerByIdCustomerInvoiceDto 
    { 
        public int Id { get; set; }
        public double Total { get; set; }
        public List<GetCustomerByIdCustomerInvoiceItemsDto> Items { get; set; }
    }
    public class GetCustomerByIdCustomerInvoiceItemsDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int ItemPrice { get; set; }
    }
}
