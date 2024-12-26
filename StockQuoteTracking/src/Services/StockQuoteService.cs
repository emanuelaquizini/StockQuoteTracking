using SkiaSharp;
using StockQuoteTracking.src.ExternalServices;
using StockQuoteTracking.src.Interfaces;
using StockQuoteTracking.src.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
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
                var body = GenerateEmailBody(stock, quotePrice, "para compra!");
                _emailService.SendEmail("Alerta de Preço Baixo para compra!", body, true);
            }
            else if (quotePrice > stock.SalePrice)
            {
                var body = GenerateEmailBody(stock, quotePrice, "para venda!");
                _emailService.SendEmail("Alerta de Preço Alto para venda!", body, true);
            }

        }

        [ExcludeFromCodeCoverage]
        private string GenerateEmailBody(Stock stock, double quotePrice, string alertType)
        {
            string chartImageBase64 = GenerateChartImageBase64(stock, quotePrice);
            string graphHtml = $@"
                <html>
                <body>
                    <h1>Alerta de Preço {alertType}</h1>
                    <p>O preço do ativo {stock.Name} está {alertType} do limite.</p>
                    <p>Preço de compra: {stock.PurchasePrice}</p>
                    <p>Preço de venda: {stock.SalePrice}</p>
                    <p>Cotação atual: {quotePrice}</p>
                    <img src='data:image/png;base64,{chartImageBase64}' alt='Gráfico de Preço' />
                </body>
                </html>";

            return graphHtml;
        }

        [ExcludeFromCodeCoverage]
        private string GenerateChartImageBase64(Stock stock, double quotePrice)
        {
            using (var surface = SKSurface.Create(new SKImageInfo(800, 600)))
            {
                var canvas = surface.Canvas;
                canvas.Clear(SKColors.White);

                var paint = new SKPaint
                {
                    Color = SKColors.Black,
                    StrokeWidth = 2,
                    IsAntialias = true,
                    TextSize = 20
                };

                var path = new SKPath();
                path.MoveTo(50, 350);
                path.LineTo(550, 350);
                path.MoveTo(50, 350);
                path.LineTo(50, 50);
                canvas.DrawPath(path, paint);


                var points = new[]
                {
                    new SKPoint(50, 350 - (float)stock.PurchasePrice),
                    new SKPoint(300, 350 - (float)quotePrice),
                    new SKPoint(550, 350 - (float)stock.SalePrice)
                };

                paint.Color = SKColors.Blue;
                paint.StrokeWidth = 4;
                canvas.DrawPoints(SKPointMode.Polygon, points, paint);

  
                paint.Color = SKColors.Black;
                paint.StrokeWidth = 2;
                canvas.DrawText($"Stock: {stock.Name}", 50, 30, paint);
                canvas.DrawText($"Purchase Price: {Math.Round(stock.PurchasePrice, 2)}", 50, 370, paint);
                canvas.DrawText($"Quote Price: {Math.Round(quotePrice, 2)}", 300, 370, paint);
                canvas.DrawText($"Sale Price: {Math.Round(stock.SalePrice, 2)}", 550, 370, paint);

                using (var image = surface.Snapshot())
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                {
                    return Convert.ToBase64String(data.ToArray());
                }
            }
        }

    }
}
