using Bogus;
using Moq;
using StockQuoteTracking.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockQuoteTracking.Tests.Services.StockQuote
{
    public class StockQuoteServiceTests : StockQuoteServiceTestsSetup
    {
        [Fact]
        public async Task TrackStockQuote_ShouldCallSymbolSearch_WithCorrectSymbol()
        {
            // Arrange
            ResetMocks();
            var service = CreateService();

            var stockSymbol = "AAPL";
            var sharePrice = "22.50";
            var purchasePrice = "50.60";

            var symbolSearchResult = new Faker<SymbolSearchResultApi>()
                .RuleFor(s => s.BestMatches, f => new List<BestMatch>
                {
                    new Faker<BestMatch>()
                        .RuleFor(b => b.Symbol, f => f.Random.String())
                        .RuleFor(b => b.Name, f => f.Company.CompanyName())
                        .RuleFor(b => b.Region, f => f.Address.Country())
                        .RuleFor(b => b.Currency, f => f.Finance.Currency().Code)
                        .Generate()
                }.ToArray())
                .Generate();

            _stockQuoteClientMoq.Setup(s => s.ObterCotacaoAtivo()).ReturnsAsync(22.50);
            _emailServiceMoq.Setup(e => e.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()));


            // Act
            service.TrackStockQuote(new string[] { stockSymbol, sharePrice , purchasePrice});

            // Assert
            _stockQuoteClientMoq.Verify(s => s.ObterCotacaoAtivo(), Times.Once);
            _emailServiceMoq.Verify(e => e.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task MonitorarCotacao_ShouldSendEmail_WhenQuotePriceExceedsThreshold()
        {
            // Arrange
            ResetMocks();
            var service = CreateService();

            var stock = new Faker<Stock>()
                .RuleFor(s => s.Name, f => f.Random.String())
                .RuleFor(s => s.SalePrice, f => f.Random.Double())
                 .RuleFor(s => s.PurchasePrice, f => f.Random.Double())
                .Generate();

            var quotePrice = 150.0;

            _emailServiceMoq.Setup(e => e.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()));

            // Act
            await service.MonitorarCotacao(stock, quotePrice);

            // Assert
            _emailServiceMoq.Verify(e => e.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task MonitorarCotacao_ShouldNotSendEmail_WhenQuotePriceIsBelowThreshold()
        {
            // Arrange
            ResetMocks();
            var service = CreateService();

            var stock = new Faker<Stock>()
                .RuleFor(s => s.Name, f => f.Random.String())
                .RuleFor(s => s.SalePrice, f => f.Random.Double(80, 100))
                 .RuleFor(s => s.PurchasePrice, f => f.Random.Double(0, 80))
                .Generate();

            var quotePrice = 80.0;

            _stockQuoteClientMoq.Setup(s => s.ObterCotacaoAtivo()).ReturnsAsync(quotePrice);


            // Act
            await service.MonitorarCotacao(stock, quotePrice);

            // Assert
            _emailServiceMoq.Verify(e => e.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
        }
    }
}
