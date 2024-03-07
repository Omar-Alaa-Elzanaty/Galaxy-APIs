namespace Galaxy.Domain.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double CurrentPurchase { get; set; }
        public double SellingPrice { get; set; }
        public int Rating { get; set; }
        public string SerialCode {  get; set; }
        public int LowInventoryIn { get; set; }
        public virtual ICollection<Stock> ItemsInStock { get; set; }
        public virtual List<CustomerInvoiceItem> CustomerInvoiceItems { get; set; }
        public virtual List<SupplierInvoiceItem> SuppliersInvoiceItems { get; set;}
    }
}
