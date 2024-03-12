namespace Galaxy.Application.Features.Customers.Querires.GetAllCustomers
{
    public class GetAllCustomersQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreationDate { get; set; }
    }
    public enum CustomerColumnName
    {
        Name = 1,
        PhoneNumber = 2,
        CreationDate = 3
    }
}
