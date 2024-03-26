namespace Galaxy.Domain.Models
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<CustomerInvoice> Invoices { get; set; }

    }
}
