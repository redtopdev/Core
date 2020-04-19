namespace Engaze.Core.Common
{
    public class KafkaConfiguration
    {
        public string BootStrapServers { get; set; }

        public int GroupId { get; set; } = 100;
    }
}
