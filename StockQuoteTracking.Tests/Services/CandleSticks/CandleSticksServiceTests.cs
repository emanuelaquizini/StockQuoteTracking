using Bogus;
using Moq;
using StockQuoteTracking.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockQuoteTracking.Tests.Services.CandleSticks
{
    public class CandleSticksServiceTests : CandleSticksServiceTestsSetup
    {
        [Fact]
        public async Task TrackCandleSticks_ShouldFormatStockSymbol()
        {
            // Arrange
            ResetMocks();
            var service = CreateService();

            var stockSymbol = "PETR4";
            var formattedSymbol = "PETR4.SA";

            var faker = new Faker<StockData>()
              .RuleFor(s => s.Date, f => f.Date.Past())
              .RuleFor(s => s.Open, f => f.Random.Double(20, 30))
              .RuleFor(s => s.High, f => f.Random.Double(30, 40))
              .RuleFor(s => s.Low, f => f.Random.Double(10, 20))
              .RuleFor(s => s.Close, f => f.Random.Double(20, 30));

            var historicalData = faker.Generate(10).ToArray();

            _stockQuoteClientMoq.Setup(s => s.GetHistoricalData(formattedSymbol)).ReturnsAsync(historicalData);
            _stockUtilsMoq.Setup(s => s.FormatStockSymbolIfBrazilian(stockSymbol)).ReturnsAsync(formattedSymbol);


            // Act
            await Task.Run(() => service.TrackCandleSticks(new string[] { stockSymbol }));

            // Assert
            _stockUtilsMoq.Verify(s => s.FormatStockSymbolIfBrazilian(stockSymbol), Times.Once);
        }

        [Fact]
        public async Task TrackCandleSticks_ShouldGetHistoricalData()
        {
            // Arrange

            ResetMocks();
            var service = CreateService();

            var stockSymbol = "PETR4";
            var formattedSymbol = "PETR4.SA";
            _stockUtilsMoq.Setup(s => s.FormatStockSymbolIfBrazilian(stockSymbol)).ReturnsAsync(formattedSymbol);

            var faker = new Faker<StockData>()
                .RuleFor(s => s.Date, f => f.Date.Past())
                .RuleFor(s => s.Open, f => f.Random.Double(20, 30))
                .RuleFor(s => s.High, f => f.Random.Double(30, 40))
                .RuleFor(s => s.Low, f => f.Random.Double(10, 20))
                .RuleFor(s => s.Close, f => f.Random.Double(20, 30));

            var historicalData = faker.Generate(10).ToArray();
            _stockQuoteClientMoq.Setup(s => s.GetHistoricalData(formattedSymbol)).ReturnsAsync(historicalData);


            // Act
            await Task.Run(() => service.TrackCandleSticks(new string[] { stockSymbol }));

            // Assert
            _stockQuoteClientMoq.Verify(s => s.GetHistoricalData(formattedSymbol), Times.Once);
        }

        [Fact]
        public void TrackCandleSticks_ShouldHandleEmptyArgs()
        {
            // Arrange
            ResetMocks();
            var service = CreateService();

            // Act & Assert
            Assert.ThrowsAsync<IndexOutOfRangeException>(() => Task.Run(() => service.TrackCandleSticks(new string[] { })));
        }
    }
}
