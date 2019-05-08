using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Engaze.Core.MessageBroker.Consumer
{
    public class EventoConsumer : BackgroundService, IDisposable
    {
        Dictionary<string, object> kafkaConfig;
        ILogger<EventoConsumer> logger;
        Consumer<Null, string> consumer;
        IMessageHandler messageHandler;

        public EventoConsumer(ILogger<EventoConsumer> logger, IMessageHandler messageHandler, Dictionary<string,string> kafkaConfig)
        {
            this.logger = logger;
            this.messageHandler = messageHandler;
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
                { "bootstrap.servers", "evento-kafka:9092" },
                { "enable.auto.commit", "false" }
            };


            consumer = new Consumer<Null, string>(kafkaConfig, null, new StringDeserializer(Encoding.UTF8));

            consumer.Subscribe(new string[] { "evento" });
            consumer.OnMessage += (_, msg) =>
            {
                try
                {
                    //this.messageHandler.OnMessageReceived(Encoding.ASCII.GetString(Convert.FromBase64String(msg.Value)));
                    this.messageHandler.OnMessageReceivedAsync(Encoding.ASCII.GetString(
                        Convert.FromBase64String(
                            JsonConvert.DeserializeObject(msg.Value).ToString())));
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
                        this.messageHandler.OnError(error.ToString());
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


            logger.LogInformation("Kafka listener started.");
            Console.WriteLine("Kafka listener started.");
            while (!stoppingToken.IsCancellationRequested)
            {
                consumer.Poll(100);
            }

            Console.WriteLine("Kafka listener stopped.");
            logger.LogInformation("Kafka listener stopped.");

            return Task.CompletedTask;
        }
    }
}