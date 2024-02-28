using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Domain.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double PurchasePrice { get; set; }
        public double CurrentPurChase { get; set; }
        public double SellingPrice { get; set; }
        public double ProfitRatio { get; set; }
        public int Rating { get; set; }
        public int LowInventoryIn { get; set; }
        public virtual ICollection<Stock> ItemsInStock { get; set; }
    }
}
