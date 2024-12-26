using StockQuoteTracking.src.Interfaces;
using StockQuoteTracking.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockQuoteTracking.src.Utils
{
    public class StockUtils : IStockUtils
    {
        private readonly IStockQuoteClient _stockQuoteClient;
        public StockUtils(IStockQuoteClient stockQuoteClient)
        {
            _stockQuoteClient = stockQuoteClient;
        }

        public async Task<string> FormatStockSymbolIfBrazilian(string stockSymbol)
        {

            if (string.IsNullOrEmpty(stockSymbol))
            {
                throw new ArgumentNullException(nameof(stockSymbol), "Stock symbol cannot be null or empty.");
            }

            var searchResult = await _stockQuoteClient.SymbolSearch(stockSymbol);

            if (searchResult?.bestMatches == null)
            {
                throw new InvalidOperationException("No matches found for the stock symbol.");
            }


            bool isFromB3 = false;

            foreach (var match in searchResult.bestMatches)
            {
                if (match.region.Contains("Brazil"))
                {
                    isFromB3= true;
                }
            }


            if (isFromB3)
            {
                return $"{stockSymbol}.SA";
            }

            return stockSymbol;
        }
    }
}
