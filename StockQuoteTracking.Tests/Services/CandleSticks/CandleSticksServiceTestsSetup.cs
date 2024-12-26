using Moq;
using StockQuoteTracking.src.Interfaces;
using StockQuoteTracking.src.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockQuoteTracking.Tests.Services.CandleSticks
{
    public class CandleSticksServiceTestsSetup
    {
        protected Mock<IStockUtils> _stockUtilsMoq;
        protected Mock<IStockQuoteClient> _stockQuoteClientMoq;

        protected CandleSticksService CreateService()
        {
            _stockUtilsMoq = new Mock<IStockUtils>(MockBehavior.Strict);
            _stockQuoteClientMoq = new Mock<IStockQuoteClient>((MockBehavior.Strict));

            return new CandleSticksService( _stockQuoteClientMoq.Object, _stockUtilsMoq.Object);
        }

        protected void ResetMocks()
        {
            _stockUtilsMoq?.Reset();
            _stockQuoteClientMoq?.Reset();
        }


    }
}
