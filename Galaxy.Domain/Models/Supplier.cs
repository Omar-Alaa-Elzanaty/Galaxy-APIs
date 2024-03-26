using Galaxy.Domain.Constants;

namespace Galaxy.Domain.Models
{
    public class Supplier : BaseEntity
    {
        public string Name { get; set; }
        public string ImageFileName { get; set; }
        public string ImageUrl => HostPath.ImageBaseUrl + ImageFileName;
        public string IdFilePath { get; set; }
        public string IdUrl => HostPath.ImageBaseUrl + IdFilePath;
        public virtual ICollection<SupplierInvoice> Invoices { get; set; }
    }
}
