using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Engaze.Core.MessageBroker.Producer
{
    public static class ConfigureServices
    {
        public static void ConfigureKafkaService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<KafkaConfiguration>(config.GetSection("KafkaConfiguration"));
            services.AddSingleton(typeof(IMessageProducer<byte[]>), typeof(KafkaProducer<byte[]>));
        }
    }
}
