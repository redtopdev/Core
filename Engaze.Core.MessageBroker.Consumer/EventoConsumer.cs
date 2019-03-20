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
    class EventoConsumer : BackgroundService, IDisposable
    {
        Dictionary<string, object> kafkaConfig;
        ILogger<EventoConsumer> logger;
        Consumer<Null, string> consumer;
        Action<string> onMessageReceived;
        Action<string> onError;
        public EventoConsumer(ILogger<EventoConsumer> logger, Action<string> onMessageReceived, Action<string> onError)
        {
            this.logger = logger;
            this.onMessageReceived = onMessageReceived;
            this.onError = onError;
        }
        public override void Dispose()
        {
            base.Dispose();
            consumer.Dispose();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            kafkaConfig = new Dictionary<string, object>
            {
                { "group.id","test" },
                { "bootstrap.servers", "localhost:9092" },
                { "enable.auto.commit", "false" }
            };


            consumer = new Consumer<Null, string>(kafkaConfig, null, new StringDeserializer(Encoding.UTF8));

            consumer.Subscribe(new string[] { "test" });
            consumer.OnMessage += (_, msg) =>
            {
                try
                {
                    onMessageReceived(msg.Value);
                }
                finally
                {
                    logger.LogInformation($"Message: {msg.Value}");
                }
            };
            consumer.OnPartitionEOF += (_, end) =>
              {
                  logger.LogInformation($"Reached end of topic {end.Topic} partition {end.Partition}, next message will be at offset {end.Offset}");
              };

            consumer.OnError += (_, error) =>
                {
                    try
                    {
                        onError(error.ToString());
                    }
                    finally
                    {
                        logger.LogError($"Error: {error}");
                    }
                };

            consumer.OnPartitionsAssigned += (_, partitions) =>
            {
                logger.LogInformation($"Assigned partitions: [{string.Join(", ", partitions)}], member id: {consumer.MemberId}");
                consumer.Assign(partitions);
            };

            consumer.OnPartitionsRevoked += (_, partitions) =>
            {
                logger.LogInformation($"Revoked partitions: [{string.Join(", ", partitions)}]");
                consumer.Unassign();
            };

            consumer.OnStatistics += (_, json)
                => logger.LogInformation($"Statistics: {json}");


            while (!stoppingToken.IsCancellationRequested)
            {
                consumer.Poll(100);
            }

            logger.LogInformation("Timed Background Service is stopping.");



            return Task.CompletedTask;
        }

    }
}
