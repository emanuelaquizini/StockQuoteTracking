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
using StockQuoteTracking.src.Utils;

namespace StockQuoteTracking.src
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var host = CreateHostBuilder(args).Build();

            var stockQuoteService = host.Services.GetService<IStockQuoteService>();
            var candleSticksService = host.Services.GetService<ICandleSticksService>();

            while (true)
            {
                // descomente ao rodar o debug para ver o terminal
                //candleSticksService.TrackCandleSticks(args);
                stockQuoteService.TrackStockQuote(args);

                await Task.Delay(20000);
            }

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
                  services.AddSingleton<ICandleSticksService, CandleSticksService>();
                  services.AddSingleton<IStockUtils, StockUtils>();

                  services.AddTransient<IEmailService, EmailService>();
              });



    }
}
