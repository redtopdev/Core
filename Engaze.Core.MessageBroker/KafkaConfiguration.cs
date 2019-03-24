namespace Engaze.Core.MessageBroker
{
    public class KafkaConfiguration
    {
        public KafkaConfiguration()
        {
            //BootStrapServers = "localhost:9092";
            BootStrapServers = "evento-kafka:9092";
        }
        public string BootStrapServers { get; set; }
    }
}
