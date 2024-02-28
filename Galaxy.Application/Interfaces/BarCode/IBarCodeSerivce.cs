using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Application.Interfaces.BarCode
{
    public interface IBarCodeSerivce
    {
        Task<long> GenerateCode();
    }
}
