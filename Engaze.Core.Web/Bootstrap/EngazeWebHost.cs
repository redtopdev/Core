using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using System.Linq;

namespace Engaze.Core.Web
{
    public class EngazeWebHost
    {
        public static void Run<T>(string[] args) where T : class
        {
            using (var host = BuildWebHost<T>(args))
            {
                if (host != null)
                {
                    host.Run();
                }
            }
        }

        /// <summary>
        /// Check whether run as service.
        /// </summary>
        /// <param name="args">The arguments for checking.</param>
        /// <returns>True indicates in service mode.</returns>
        private static bool IsServiceMode(string[] args)
        {
            if (System.Diagnostics.Debugger.IsAttached || (args != null && args.Contains("--console")))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Build web host.
        /// </summary>
        /// <returns>web host.</returns>
        private static IWebHost BuildWebHost<T>(string[] args) where T : class
        {
            var baseRoot = Directory.GetCurrentDirectory();

            if (IsServiceMode(args))
            {
                var exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                baseRoot = Path.GetDirectoryName(exePath);
            }

            var config = new ConfigurationBuilder()
           .SetBasePath(baseRoot)
           .AddJsonFile("appsettings.json", optional: true)
           .AddEnvironmentVariables()
           .Build();


            var url = config["ASPNETCORE_URLS"] ?? "http://*:5000";
            var env = config["ASPNETCORE_ENVIRONMENT"] ?? "Development";

            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(config)
               .CreateLogger();
            try
            {
                Log.Information("Application starting up. ");

                Console.WriteLine(baseRoot);

                var webHostBuilder = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(url)
                .UseSerilog()
                .UseEnvironment(env)
                .UseContentRoot(baseRoot)
                .UseConfiguration(config);
                webHostBuilder.ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddSerilog(Log.Logger);
                    if (env.Equals("Development"))
                    {
                        logging.AddConsole();
                        logging.AddDebug();
                    }
                });

                return webHostBuilder.UseStartup<T>().Build();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
                return null;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
