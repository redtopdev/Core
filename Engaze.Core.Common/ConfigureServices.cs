using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Engaze.Core.Common
{
    public static class ConfigureServices
    {
        public static void ConfigureKafkaService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<KafkaConfiguration>(config.GetSection("KafkaConfiguration"));
            services.AddSingleton(typeof(KafkaConfiguration), services.BuildServiceProvider().GetService<IOptions<KafkaConfiguration>>().Value);

        }
    }
}
