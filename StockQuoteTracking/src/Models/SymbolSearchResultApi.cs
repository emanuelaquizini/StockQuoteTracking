using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockQuoteTracking.src.Models
{
    public class SymbolSearchResultApi
    {
        public BestMatch[] BestMatches { get; set; }
    }


    public class BestMatch
    {
        [JsonProperty("1. symbol")]
        public string Symbol { get; set; }
        [JsonProperty("2. name")]
        public string Name { get; set; }
        [JsonProperty("4. region")]
        public string Region { get; set; }
        [JsonProperty("8. currency")]
        public string Currency { get; set; }
    }

}
