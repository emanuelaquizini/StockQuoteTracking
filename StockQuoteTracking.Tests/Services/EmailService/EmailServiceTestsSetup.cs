using Moq;
using StockQuoteTracking.src.Interfaces;
using StockQuoteTracking.src.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockQuoteTracking.Tests.Services.EmailService
{

    public class EmailServiceTestsSetup
    {
        protected Mock<IConfig> _configMoq;


        protected src.Services.EmailService CreateService()
        {
            _configMoq = new Mock<IConfig>(MockBehavior.Strict);
            return new src.Services.EmailService(_configMoq.Object);
        }

        protected void ResetMocks()
        {
            _configMoq?.Reset();
        }


    }
}
