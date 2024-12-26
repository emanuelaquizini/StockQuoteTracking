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
        private readonly IEmailService _emailService;
        public StockQuoteService(IStockQuoteClient stockQuoteClient, IEmailService emailService)
        {
            _stockQuoteClient = stockQuoteClient;
            _emailService = emailService;
        }

        public async void TrackStockQuote(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("You need to send the three arguments! \n " +
                    "Example: StockQuoteTracking.exe <stock><sale-price><purchase-price>");

                throw new Exception("You need to send the three arguments! Example: StockQuoteTracking.exe <stock><sale-price><purchase-price>");
            }

            string stockName = args[0];
            double salePrice = double.Parse(args[1], System.Globalization.CultureInfo.InvariantCulture);
            double purchasePrice = double.Parse(args[2], System.Globalization.CultureInfo.InvariantCulture);

            Stock stock = new Stock(stockName, salePrice, purchasePrice);

            Console.WriteLine(JsonSerializer.Serialize(stock));

            double quotePrice = await _stockQuoteClient.ObterCotacaoAtivo();
            await MonitorarCotacao(stock, quotePrice);

        }


        public async Task MonitorarCotacao(Stock stock, double quotePrice)
        {

            if (quotePrice < stock.PurchasePrice)
            {
                _emailService.SendEmail("Alerta de Preço Baixo", $"O preço do ativo {stock.Name} caiu abaixo do limite de compra ({stock.PurchasePrice}). Cotação atual: {quotePrice}");
            }
            else if (quotePrice > stock.SalePrice)
            {
                _emailService.SendEmail("Alerta de Preço Alto", $"O preço do ativo {stock.Name} subiu acima do limite de venda ({stock.SalePrice}). Cotação atual: {quotePrice}");
            }

        }

    }
}
