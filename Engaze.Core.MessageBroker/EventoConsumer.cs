using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Engaze.Core.MessageBroker.Producer
{
    class EventoConsumer : IHostedService, IDisposable
    {
        Dictionary<string, object> kafkaConfig;
        ILogger<EventoConsumer> logger;
        private Timer timer;
        Consumer<Null, string> consumer;
        public EventoConsumer(ILogger<EventoConsumer> logger)
        {
            this.logger = logger;
        }
        public void Dispose()
        {
            consumer.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            kafkaConfig = new Dictionary<string, object>
            {
                { "group.id","booking_consumer" },
                { "bootstrap.servers", "localhost:9092" },
                { "enable.auto.commit", "false" }
            };


            using (var consumer = new Consumer<Null, string>(kafkaConfig, null, new StringDeserializer(Encoding.UTF8)))
            {
                consumer.Subscribe("timemanagement_booking");
                consumer.OnMessage += (_, msg) =>
                {
                    logger.LogInformation(msg.Value);
                };
            }

            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(1000));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            consumer.Poll(100);
            logger.LogInformation("Timed Background Service is working.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {           
            logger.LogInformation("Timed Background Service is stopping.");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
