namespace Galaxy.Domain.Models
{
    public class Stock : BaseEntity
    {
        public string BarCode { get; set; }
        public bool IsInStock { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int? SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

    }
}
