using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Pharamcy.Application.Interfaces.Media
{
    public interface IMediaService
    {
        Task DeleteAsync(string url);
        Task<string> SaveAsync(IFormFile media);
        Task<string> UpdateAsync(string oldUrl, IFormFile newMedia);
    }
}
