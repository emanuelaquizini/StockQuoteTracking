using Moq;
using StockQuoteTracking.src.Interfaces;
using StockQuoteTracking.src.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockQuoteTracking.Tests.Services.StockQuote
{
    public class StockQuoteServiceTestsSetup
    {

        protected Mock<IStockQuoteClient> _stockQuoteClientMoq;
        protected Mock<IEmailService> _emailServiceMoq;


        protected StockQuoteService CreateService()
        {
            _stockQuoteClientMoq = new Mock<IStockQuoteClient>(MockBehavior.Strict);
            _emailServiceMoq = new Mock<IEmailService>(MockBehavior.Strict);

            return new StockQuoteService(_stockQuoteClientMoq.Object, _emailServiceMoq.Object);
        }

        protected void ResetMocks()
        {
            _stockQuoteClientMoq?.Reset();
            _emailServiceMoq?.Reset();


        }
    }
}
