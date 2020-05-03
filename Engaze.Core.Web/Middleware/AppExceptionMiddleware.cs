using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Engaze.Core.Web
{
    public class AppExceptionMiddleware
    {
        private ILogger<AppExceptionMiddleware> logger;
        private readonly RequestDelegate next;

        public AppExceptionMiddleware(RequestDelegate next, ILogger<AppExceptionMiddleware> logger)
        {
            this.logger = logger;
            this.next = next;
        }
       
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
               
                await next(httpContext); // calling next middleware
            }
           catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());//temporarly
                logger.LogError(ex.ToString(), null);
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsync("Currently unable to process the request due to internal exception. Please try after some time");

            }

        }
    }
}
