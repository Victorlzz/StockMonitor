using GenericHost.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;

namespace GenericHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            string environmentName = string.Empty;
            if (args.Length > 0)
                environmentName = args[0];

            string path = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath);

            IHostBuilder host = Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(config =>
                {
                    config.SetBasePath(path);
                    config.AddCommandLine(args);

                    if (string.IsNullOrEmpty(environmentName))
                        config.AddJsonFile("hostsettings.json", optional: true);
                    else
                        config.AddJsonFile($"hostsettings.{environmentName}.json", optional: true);
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddGatewayServiceLocator();
                    services.AddStockServiceLocator();
                    services.AddHostedService<LifetimeEventsHostedService>();
                    //services.AddSingleton<IGatewayServiceProvider, GatewayServiceProvider>();

                });

            return host;
        }
    }
}
