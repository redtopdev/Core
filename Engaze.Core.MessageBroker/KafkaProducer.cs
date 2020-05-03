using Confluent.Kafka;
using Engaze.Core.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Engaze.Core.MessageBroker
{
    public class KafkaProducer<T> : IMessageProducer<T> where T : class
    {
        private readonly ProducerConfig config;


        private ILogger<KafkaProducer<T>> logger;
        private KafkaConfiguration kafkaConfiguration;

        //private readonly ILogger logger;
        public KafkaProducer(IOptions<KafkaConfiguration> options, ILogger<KafkaProducer<T>> logger)
        {
            this.kafkaConfiguration = options.Value;
            this.logger = logger;

            this.config = new ProducerConfig
            {
                BootstrapServers = kafkaConfiguration.BootStrapServers,
                SaslMechanism = SaslMechanism.Plain,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslUsername = kafkaConfiguration.SaslUsername,
                SaslPassword = kafkaConfiguration.SaslPassword
            };
        }
        public void Write(T message, string topic)
        {
            using (var producer = new ProducerBuilder<Null, string>(this.config).Build())
            {
                var dr = producer.ProduceAsync(topic, new Message<Null, string> { Value = JsonConvert.SerializeObject(message) }).Result;
                logger.LogInformation($"Delivered '{dr.Value}' to: {dr.TopicPartitionOffset}");
            }
        }

        public async Task<DeliveryResult<Null, string>> WriteAsync(T message, string topic)
        {
            using (var producer = new ProducerBuilder<Null, string>(this.config).Build())
            {
                return await producer.ProduceAsync(topic, new Message<Null, string> { Value = JsonConvert.SerializeObject(message) });
               
            }
        }
    }
}