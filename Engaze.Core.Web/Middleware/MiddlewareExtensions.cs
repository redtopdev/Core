
namespace Engaze.Core.Web
{
    using Microsoft.AspNetCore.Builder;
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseAppException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AppExceptionMiddleware>();
        }

        public static IApplicationBuilder UseAppStatus(this IApplicationBuilder builder)
            =>
            builder.MapWhen(context => context.Request.Method == "GET" && context.Request.Path.Equals("/service-status"), appBuilder =>
              {
                  appBuilder.UseMiddleware<AppStatusMiddleware>();
              });

        public static IApplicationBuilder UseCorrelationHeader(this IApplicationBuilder builder)
            => builder.UseMiddleware<CorrelationHeaderMiddleware>();


        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
            => builder.UseMiddleware<RequestResponseLoggingMiddleware>();
    }
}
