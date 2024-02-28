using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double ProfitRatio { get; set; }
    }
}
