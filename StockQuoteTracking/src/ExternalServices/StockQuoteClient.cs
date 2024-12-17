using StockQuoteTracking.src.Interfaces;
using StockQuoteTracking.src.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace StockQuoteTracking.src.ExternalServices
{
    public class StockQuoteClient : IStockQuoteClient
    {
        private readonly HttpClient _httpClient;

        public StockQuoteClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task GetStockQuotePriceAsync(Stock stock)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=IBM&apikey=demo";
            Uri queryUri = new Uri(QUERY_URL);

            try
            {
                string jsonString = await _httpClient.GetStringAsync(queryUri);
                var jsonData = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(jsonString);

                Console.WriteLine("Stock quote data retrieved successfully.");
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine($"Request error: {httpRequestException.Message}");
            }
            catch (JsonException jsonException)
            {
                Console.WriteLine($"JSON error: {jsonException.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            //// TODO: pegar valores cotacao
        }
    }
}
