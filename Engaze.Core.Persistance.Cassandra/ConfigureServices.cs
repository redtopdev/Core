using Cassandra;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.IO;
using System.Reflection;

namespace Engaze.Core.Persistance.Cassandra
{
    public static class ConfigureServices
    {
        public static void ConfigureCassandra(this IServiceCollection services, IConfiguration config)
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

        public static void ConfigureCloudCassandra(this IServiceCollection services, IConfiguration config, string secureConnectionZipFilePath = null)
        {
            if (string.IsNullOrEmpty(secureConnectionZipFilePath))
            {
                secureConnectionZipFilePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            }

            if (!secureConnectionZipFilePath.ToUpper().EndsWith(".ZIP"))
            {
                secureConnectionZipFilePath = Path.Combine(secureConnectionZipFilePath, "secure-connect-engaze.zip");
            }
            services.Configure<CassandraConfiguration>(config.GetSection("CassandraConfiguration"));
            var options = services.BuildServiceProvider().GetService<IOptions<CassandraConfiguration>>();

            CassandraConfiguration cassandraConfig = options.Value;
            var cluster = Cluster.Builder()
                .WithCloudSecureConnectionBundle(secureConnectionZipFilePath)
                .WithCredentials(cassandraConfig.UserName, cassandraConfig.Password)
                .Build();
            CassandraSessionCacheManager cassandraSessionCacheManager = new CassandraSessionCacheManager(cluster);
            services.AddSingleton(cluster.GetType(), cluster);
            services.AddSingleton(cassandraSessionCacheManager.GetType(), cassandraSessionCacheManager);
            services.AddSingleton(cassandraConfig.GetType(), cassandraConfig);
        }
    }
}
