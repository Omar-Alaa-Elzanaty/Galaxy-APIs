namespace Galaxy.Domain.Models
{
    public class CustomerInvoice : BaseEntity
    {
        public double Total { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<CustomerInvoiceItem> Items { get; set; }
    }
}
