using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockQuoteTracking.src.Configurations;
using StockQuoteTracking.src.ExternalServices;
using StockQuoteTracking.src.Interfaces;
using StockQuoteTracking.src.Models;
using StockQuoteTracking.src.Services;

namespace StockQuoteTracking.src
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
           
            var host = CreateHostBuilder(args).Build();
       
            var stockQuoteService = host.Services.GetService<IStockQuoteService>();

            stockQuoteService.TrackStockQuote(args);
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
            .ConfigureHostConfiguration(configHost =>
            {
                configHost.AddEnvironmentVariables();
                configHost.AddCommandLine(args);    
            })
              .ConfigureLogging(logging =>
              {
                  logging.AddConsole();
              })
              .ConfigureServices((hostContext, services) =>
              {
                  services.AddSingleton<IStockQuoteService, StockQuoteService>();
                  services.AddSingleton<IStockQuoteClient, StockQuoteClient>();
                  services.AddSingleton<IConfig, Config>();

                  services.AddTransient<IEmailService, EmailService>();
              });



    }
}
