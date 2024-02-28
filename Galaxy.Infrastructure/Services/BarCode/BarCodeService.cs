using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Application.Interfaces.BarCode;

namespace Galaxy.Infrastructure.Services.BarCode
{
    public class BarCodeService : IBarCodeSerivce
    {
        public async Task<long> GenerateCode()
        {
            await Task.CompletedTask;

            //TODO: Generate Barcode

            return new Random().NextInt64();
        }
    }
}
