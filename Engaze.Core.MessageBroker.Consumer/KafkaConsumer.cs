using Confluent.Kafka;
using Engaze.Core.Common;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Engaze.Core.MessageBroker.Consumer
{
    public class KafkaConsumer : BackgroundService, IDisposable
    {
       
        private ILogger<KafkaConsumer> logger;
       
        private IMessageHandler messageHandler;
        private KafkaConfiguration kafkaConfig;
        private ConsumerConfig consumerConfig;

        public KafkaConsumer(ILogger<KafkaConsumer> logger, KafkaConfiguration kafkaConfig, IMessageHandler messageHandler)
        {

            this.logger = logger;
            this.messageHandler = messageHandler;
            this.kafkaConfig = kafkaConfig;            

            consumerConfig = new ConsumerConfig
            {
                BootstrapServers = kafkaConfig.BootStrapServers,
                SaslMechanism = SaslMechanism.Plain,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslUsername = kafkaConfig.SaslUsername,
                SaslPassword = kafkaConfig.SaslPassword,
                GroupId = kafkaConfig.ConsumerGroupId.ToString(),
                AutoOffsetReset = AutoOffsetReset.Earliest                
            };
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build())
            {
                consumer.Subscribe(this.kafkaConfig.Topic);
                try
                {
                    var consumeResult = consumer.Consume();

                    this.messageHandler.OnMessageReceivedAsync(consumeResult.Message.Value);
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"consume error: {e.Error.Reason}");
                    this.logger.LogError(e.ToString());
                }
                catch(Exception e)
                {
                    Console.WriteLine($"error: {e.ToString()}");
                    this.logger.LogError(e.ToString());
                }

                return Task.CompletedTask;
            }
        }
    }
}