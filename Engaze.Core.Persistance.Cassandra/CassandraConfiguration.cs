namespace Engaze.Core.Persistance.Cassandra
{
    public class CassandraConfiguration
    {
        public string ContactPoint { get; set; }
        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string KeySpace { get; set; }
    }
}
