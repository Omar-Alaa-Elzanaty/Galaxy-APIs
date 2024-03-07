using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Domain.Models;

namespace Galaxy.Application.Interfaces.BarCode
{
    public interface IBarCodeSerivce
    {
        Task<string> GenerateItemCode(int productId,string yearCode,int quantity);
        string CompleteString(string code, int totalLength);
    }
}
