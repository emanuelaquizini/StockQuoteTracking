using StockQuoteTracking.src.Interfaces;
using StockQuoteTracking.src.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockQuoteTracking.src.Services
{
    public class CandleSticksService : ICandleSticksService
    {
        private readonly IStockQuoteClient _stockQuoteClient;
        private readonly IStockUtils _stockUtils;
        public CandleSticksService(IStockQuoteClient stockQuoteClient, IStockUtils stockUtils){
            _stockQuoteClient = stockQuoteClient;
            _stockUtils = stockUtils;
        }

        public async void TrackCandleSticks(string[] args)
        {
            

            Console.WriteLine("Candlesticks for " + args[0]);
            string stockName = args[0];

            string formattedSymbol = await _stockUtils.FormatStockSymbolIfBrazilian(stockName);

            var historicalData = await _stockQuoteClient.GetHistoricalData(formattedSymbol);

            double maxPrice = double.MinValue;
            double minPrice = double.MaxValue;

            foreach (var day in historicalData)
            {
                maxPrice = Math.Max(maxPrice, day.High);
                minPrice = Math.Min(minPrice, day.Low);
            }

            int graphHeight = 10;  

            foreach (var day in historicalData)
            {
                int openHeight = (int)((day.Open - minPrice) / (maxPrice - minPrice) * graphHeight);
                int closeHeight = (int)((day.Close - minPrice) / (maxPrice - minPrice) * graphHeight);
                int highHeight = (int)((day.High - minPrice) / (maxPrice - minPrice) * graphHeight);
                int lowHeight = (int)((day.Low - minPrice) / (maxPrice - minPrice) * graphHeight);

                Console.WriteLine(new string(' ', openHeight) + "█" + new string(' ', highHeight - openHeight) + "|");
                Console.WriteLine(new string(' ', lowHeight) + "█" + new string(' ', closeHeight - lowHeight) + "|");
                Console.WriteLine("--------------------------------------------------");
            }
        }
    }
}
