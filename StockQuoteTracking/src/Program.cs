using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using StockQuoteTracking.src.ExternalServices;
using StockQuoteTracking.src.Interfaces;
using StockQuoteTracking.src.Models;
using StockQuoteTracking.src.Services;

namespace StockQuoteTracking.src
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var stockQuoteService = serviceProvider.GetService<IStockQuoteService>();


            stockQuoteService.TrackStockQuote(args);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IStockQuoteService, StockQuoteService>();
            services.AddSingleton<IStockQuoteClient, StockQuoteClient>();

        }
    }
}
