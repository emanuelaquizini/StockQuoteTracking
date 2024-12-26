using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockQuoteTracking.src.Models
{
    public class AlphaVantageResponse
    {
        [JsonProperty("Time Series (Daily)")]
        public Dictionary<string, Dictionary<string, string>> TimeSeries { get; set; }
    }
}
