using Engaze.Core.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Engaze.Core.MessageBroker.Consumer
{
    public static class ConfigureServices
    {
        public static void ConfigureConsumerService(this IServiceCollection services, IConfiguration config, IMessageHandler messageHandler)
        {
            services.ConfigureKafkaService(config);
            services.AddSingleton(typeof(IMessageHandler), messageHandler);
        }
    }
}
