using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Engaze.Core.Web
{
    public class AppStatusMiddleware
    {
        private readonly DateTime startTime;
        public AppStatusMiddleware(RequestDelegate next) 
        {
            startTime = DateTime.UtcNow;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            TimeSpan span = (DateTime.UtcNow - startTime);          

            await httpContext.Response.WriteAsync(String.Format("Service is up and running since {0}D_{1}H_{2}M_{3}S",
                span.Days, span.Hours, span.Minutes, span.Seconds));

        }
    }
}
