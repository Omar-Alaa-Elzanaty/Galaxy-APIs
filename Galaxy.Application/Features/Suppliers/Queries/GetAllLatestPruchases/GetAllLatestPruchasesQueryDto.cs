﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Application.Features.Suppliers.Queries.GetAllLatestPruchases
{
    public class GetAllLatestPruchasesQueryDto
    {
        public int Id { get; set; }
        public string Name {  get; set; }
        public double? TotalPay { get; set; }
        public DateTime? Date { get; set; }
    }
}