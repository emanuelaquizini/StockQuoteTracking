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
        public BestMatch[] bestMatches { get; set; }
    }


    public class BestMatch
    {
        [JsonProperty("1. symbol")]
        public string symbol { get; set; }
        [JsonProperty("2. name")]
        public string name { get; set; }
        [JsonProperty("4. region")]
        public string region { get; set; }
        [JsonProperty("8. currency")]
        public string currency { get; set; }
    }

}
