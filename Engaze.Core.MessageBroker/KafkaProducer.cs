using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Engaze.Core.MessageBroker
{
    public class KafkaProducer<T> : IMessageProducer<T> where T : class
    {
        private readonly Dictionary<string, object> config;


        private ILogger<KafkaProducer<T>> logger;
        private KafkaConfiguration kafkaConfiguration;

        //private readonly ILogger logger;
        public KafkaProducer(KafkaConfiguration kafkaConfiguration, ILogger<KafkaProducer<T>> logger)
        {
            this.kafkaConfiguration = kafkaConfiguration;
            this.logger = logger;

            this.config = new Dictionary<string, object>
            {
                { "bootstrap.servers", kafkaConfiguration.BootStrapServers }
            };
        }
        public void Write(T message, string topic)
        {
            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {
                var dr = producer.ProduceAsync(topic, null, JsonConvert.SerializeObject(message)).Result;
                logger.LogInformation($"Delivered '{dr.Value}' to: {dr.TopicPartitionOffset}");
            }
        }

        public async Task<Message<Null, string>> WriteAsync(T message, string topic)
        {
            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {
                return await producer.ProduceAsync(topic, null, JsonConvert.SerializeObject(message));
            }

        }
    }
}