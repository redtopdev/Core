﻿namespace Engaze.Core.MessageBroker.Consumer
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.DependencyInjection;
    using System;
   


    class Program
    {
        public static void Main(string[] args)
        {
            new HostBuilder().ConfigureHostConfiguration(configHost =>
            {
                configHost.AddCommandLine(args);
                configHost.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureAppConfiguration((hostContext, configApp) =>
            {
                hostContext.HostingEnvironment.EnvironmentName = System.Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            })
             .ConfigureLogging((hostContext, configLogging) =>
             {
                 if (hostContext.HostingEnvironment.IsDevelopment())
                 {
                     configLogging.AddConsole();
                     configLogging.AddDebug();
                 }
             }).ConfigureServices((hostContext, services) =>
             {
                 services.AddLogging();
                 services.AddSingleton<EventStreamListener>();
                 services.AddSingleton<IMessageHandler>();
                 services.AddHostedService<SubscriberService>();

             })
             .RunConsoleAsync();

            Console.ReadLine();
        }
    }
}


