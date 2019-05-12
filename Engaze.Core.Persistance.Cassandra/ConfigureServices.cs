using Cassandra;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Engaze.Core.Persistance.Cassandra
{
    public static class ConfigureServices
    {
        public static void ConfigureCassandraServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CassandraConfiguration>(config.GetSection("CassandraConfiguration"));            
            var options = services.BuildServiceProvider().GetService<IOptions<CassandraConfiguration>>();

            CassandraConfiguration cassandraConfig = options.Value;
            var cluster = Cluster.Builder()
                .AddContactPoint(cassandraConfig.ContactPoint)
                .WithPort(cassandraConfig.Port)
                .WithCredentials(cassandraConfig.UserName, cassandraConfig.Password)
                .Build();
            CassandraSessionCacheManager cassandraSessionCacheManager = new CassandraSessionCacheManager(cluster);
            services.AddSingleton(cluster.GetType(), cluster);
            services.AddSingleton(cassandraSessionCacheManager.GetType(), cassandraSessionCacheManager);
            services.AddSingleton(cassandraConfig.GetType(), cassandraConfig);
        }
    }
}
