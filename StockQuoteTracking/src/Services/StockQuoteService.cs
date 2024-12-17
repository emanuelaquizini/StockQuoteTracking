using StockQuoteTracking.src.ExternalServices;
using StockQuoteTracking.src.Interfaces;
using StockQuoteTracking.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StockQuoteTracking.src.Services
{
    public class StockQuoteService : IStockQuoteService
    {
        private readonly IStockQuoteClient _stockQuoteClient;
        public StockQuoteService(IStockQuoteClient stockQuoteClient)
        {
            _stockQuoteClient = stockQuoteClient;
        }

        public async void TrackStockQuote(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("You need to send the three arguments! \n " +
                    "Example: StockQuoteTracking.exe <stock><sale-price><purchase-price>");
                return;
            }

            string stockName = args[0];
            double salePrice = double.Parse(args[1]);
            double purchasePrice = double.Parse(args[2]);

            Stock stock = new Stock(stockName, salePrice, purchasePrice);

            Console.WriteLine(JsonSerializer.Serialize(stock));

            await _stockQuoteClient.GetStockQuotePriceAsync(stock);
        }

    }
}
