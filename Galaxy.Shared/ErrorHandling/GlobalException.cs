using Galaxy.Shared;
using Microsoft.AspNetCore.Http;

namespace Pharamcy.Shared.ErrorHandling
{
    public abstract class GlobalException : Exception
    {
        public GlobalException(string message) : base(message) { }
        public abstract Task HandleExceptionAsync(HttpContext context, Response response);
    }
}
