namespace Engaze.Core.Common
{
    public class KafkaConfiguration
    {
        public string Topic { get; set; }
        public string BootStrapServers { get; set; }

        public int ConsumerGroupId { get; set; } = 100;

        public string SaslMechanism { get; set; }

        public string SecurityProtocol { get; set; }

        public string SaslUsername { get; set; }

        public string SaslPassword { get; set; }
    }
}
