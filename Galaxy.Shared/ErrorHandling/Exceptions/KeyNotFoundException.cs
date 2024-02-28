using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Shared;
using Microsoft.AspNetCore.Http;

namespace Pharamcy.Shared.ErrorHandling.Exceptions
{
    public class KeyNotFoundException : GlobalException
    {
        private readonly string _message;
        public KeyNotFoundException(string message) : base(message)
        {
            _message = message;
        }

        public override async Task HandleExceptionAsync(HttpContext context, Response response)
        {
            response.Message = _message;
            response.StatusCode = HttpStatusCode.NotFound;

            await Task.CompletedTask;
        }
    }
}
