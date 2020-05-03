using Engaze.Core.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Engaze.Core.MessageBroker.Producer
{
    public static class ConfigureServices
    {
        public static void ConfigureProducerService(this IServiceCollection services, IConfiguration config )
        {
            services.ConfigureKafkaService(config);
            services.AddSingleton(typeof(IMessageProducer<dynamic>), typeof(KafkaProducer<dynamic>));
        }
    }
}
