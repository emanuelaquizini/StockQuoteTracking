using Newtonsoft.Json;
using StockQuoteTracking.src.Interfaces;
using StockQuoteTracking.src.Models;
using StockQuoteTracking.src.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace StockQuoteTracking.src.ExternalServices
{
    public class StockQuoteClient : IStockQuoteClient
    {
        private readonly IConfig _config;
        public StockQuoteClient(IConfig config)
        {
            _config = config;
        }

        public async Task<double> ObterCotacaoAtivo()
        {
            
            Random rand = new Random();
            return (rand.NextDouble() * 100);
        }

        public async Task<StockData[]> GetHistoricalData(string symbol)
        {
            StockApiSettings settings = _config.LoadStockApiSettings();

            string url = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={settings.ApiKey}&outputsize=full";

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var data = JsonConvert.DeserializeObject<AlphaVantageResponse>(response);
                var timeSeries = data.TimeSeries;

                if (timeSeries == null)
                {
                    throw new InvalidOperationException("No historical data found for the stock symbol.");
                }


                var stockData = new List<StockData>();
                foreach (var item in timeSeries)
                {
                    stockData.Add(new StockData
                    {
                        Date = DateTime.Parse(item.Key),
                        Open = double.Parse(item.Value["1. open"], System.Globalization.CultureInfo.InvariantCulture),
                        High = double.Parse(item.Value["2. high"], System.Globalization.CultureInfo.InvariantCulture),
                        Low = double.Parse(item.Value["3. low"], System.Globalization.CultureInfo.InvariantCulture),
                        Close = double.Parse(item.Value["4. close"], System.Globalization.CultureInfo.InvariantCulture)
                    });
                }

                return stockData.ToArray();
            }
        }

        public async Task<SymbolSearchResultApi> SymbolSearch(string stockSymbol)
        {

            StockApiSettings settings = _config.LoadStockApiSettings();

            string url = $"https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords={stockSymbol}&apikey={settings.ApiKey}";

            Console.WriteLine("Url aquiii " + url);

            using (var client = new HttpClient())
            {


                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                SymbolSearchResultApi searchResult = JsonConvert.DeserializeObject<SymbolSearchResultApi>(responseBody);

                if (searchResult == null)
                {
                    throw new InvalidOperationException("No matches found for the stock symbol.");
                }

                return searchResult;
            }
        }


    }
}
