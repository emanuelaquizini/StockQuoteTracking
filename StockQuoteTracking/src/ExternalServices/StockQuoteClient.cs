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

        public StockQuoteClient()
        {
        }

        public async Task<double> ObterCotacaoAtivo()
        {
            
            Random rand = new Random();
            return (rand.NextDouble() * 100); // Simula uma cotação entre 0 e 100
        }

       
 
     
    }
}
