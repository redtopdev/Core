using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Engaze.Core.Common
{
    public static class ConfigureServices
    {
        public static void ConfigureKafkaService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<KafkaConfiguration>(config.GetSection("KafkaConfiguration"));            
        }
    }
}
