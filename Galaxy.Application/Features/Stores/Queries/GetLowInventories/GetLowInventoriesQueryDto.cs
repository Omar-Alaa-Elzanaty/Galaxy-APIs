using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Application.Features.Stores.Queries.GetLowInventories
{
    public class GetLowInventoriesQueryDto
    {
        public int ProdcutId { get; set; }
        public string ProdcutName { get; set; }
        public int CurrentAmount { get; set; }
        public int LowLimit { get; set; }
    }
}
