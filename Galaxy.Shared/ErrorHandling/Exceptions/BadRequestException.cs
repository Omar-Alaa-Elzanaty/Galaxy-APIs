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
    public class BadRequestException : GlobalException
    {
        private readonly string _message;
        public BadRequestException(string message) : base(message)
        {
            _message = message;
        }

        public override Task HandleExceptionAsync(HttpContext context, Response response)
        {
            response.Message = _message;
            response.StatusCode = HttpStatusCode.BadRequest;

            return Task.CompletedTask;
        }
    }
}
