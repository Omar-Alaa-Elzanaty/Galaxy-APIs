using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Pharamcy.Application.Interfaces.Media;

namespace Pharamcy.Infrastructure.Media
{
    public class MediaServices : IMediaService
    {
        private readonly IWebHostEnvironment _host;
        private readonly HttpContext _httpContext;

        public MediaServices(
            IWebHostEnvironment host,
            IConfiguration configuration,
            IHttpContextAccessor contextAccessor)
        {
            _host = host;
            _httpContext = contextAccessor.HttpContext!;
        }

        public async Task DeleteAsync(string url)
        {
            string RootPath = _host.WebRootPath.Replace("\\\\", "\\");
            var imageNameToDelete = Path.GetFileNameWithoutExtension(url);
            var EXT = Path.GetExtension(url);
            var oldImagePath = $@"{RootPath}\Images\{imageNameToDelete}{EXT}";
            if (File.Exists(oldImagePath))
            {
                File.Delete(oldImagePath);
            }
            await Task.CompletedTask;
        }

        public async Task<string> SaveAsync(IFormFile media)
        {
            var extension = Path.GetExtension(media.FileName).ToLower();

            var uniqueFileName = Guid.NewGuid().ToString() + extension;

            var uploadsFolder = Path.Combine("wwwroot", "Images");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                media.CopyTo(stream);
                stream.Dispose();
            }

            var baseUrl = @$"{_httpContext.Request.Scheme}://{_httpContext.Request.Host}/Images/";

            return baseUrl + uniqueFileName;
        }

        public async Task<string> UpdateAsync(string oldUrl, IFormFile newMedia)
        {
            await DeleteAsync(oldUrl);
            return await SaveAsync(newMedia);
        }

    }
}
