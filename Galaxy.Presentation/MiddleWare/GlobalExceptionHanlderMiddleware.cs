using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Galaxy.Shared;
using Microsoft.AspNetCore.Http;
using Pharamcy.Shared.ErrorHandling;


namespace Pharamcy.Presentation.Middleware
{
    public class GlobalExceptionHanlderMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHanlderMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {

                    var response = new Response();
                    response.IsSuccess = false;
                    response.Message = "Unauthorize";
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    var jsonOptions = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };
                    var exceptionResult = JsonSerializer.Serialize(response, jsonOptions);


                    await context.Response.WriteAsync(exceptionResult);
                    return;
                }

            }
            catch (GlobalException ex)
            {
                await HandlingExceptionAsync(context, ex);
            }
        }
        private static Task HandlingExceptionAsync(HttpContext context, GlobalException exception)
        {
            var response = new Response() { IsSuccess = false };

            exception.HandleExceptionAsync(context, response);
            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var exceptionResult = JsonSerializer.Serialize(response, jsonOptions);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
