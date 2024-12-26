using Moq;
using StockQuoteTracking.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockQuoteTracking.Tests.Services.EmailService
{
    public class EmailServiceTests : EmailServiceTestsSetup
    {
        [Fact]
        public void SendEmail_ShouldInvokeLoadEmailSettings()
        {
            // Arrange
            ResetMocks();
            var service = CreateService();

            var emailSubject = "Test Subject";
            var emailBody = "Test Body";
            var emailSettings = new EmailSettings { /* initialize properties */ };

            _configMoq.Setup(c => c.LoadEmailSettings()).Returns(emailSettings);


            // Act
            service.SendEmail(emailSubject, emailBody);

            // Assert
            _configMoq.Verify(c => c.LoadEmailSettings(), Times.Once);
        }

        [Fact]
        public void SendEmail_ShouldThrowException_WhenEmailSettingsAreNull()
        {
            // Arrange
            ResetMocks();
            var service = CreateService();

            var emailSubject = "Test Subject";
            var emailBody = "Test Body";

            _configMoq.Setup(c => c.LoadEmailSettings()).Returns((EmailSettings)null);


            // Act & Assert
            Assert.Throws<NullReferenceException> (() => service.SendEmail(emailSubject, emailBody));
        }

    }
}
