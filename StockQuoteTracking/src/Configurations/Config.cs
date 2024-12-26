using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using StockQuoteTracking.src.Interfaces;
using StockQuoteTracking.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockQuoteTracking.src.Configurations
{
    public class Config : IConfig
    {
        private readonly IHostEnvironment _hostEnvironment;

        public Config(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }


        public EmailSettings LoadEmailSettings()
        {
            EmailSettings emailSettings = new EmailSettings();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_hostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var emailSettingsConfig = configuration.GetSection("EmailSettings");

            if (emailSettings is not null)
            {
                emailSettings.SmtpPassword = emailSettingsConfig.GetSection("SmtpPassword").Value;
                emailSettings.SmtpPort = int.Parse(emailSettingsConfig.GetSection("SmtpPort").Value);
                emailSettings.SmtpServer = emailSettingsConfig.GetSection("SmtpServer").Value;
                emailSettings.SmtpUser = emailSettingsConfig.GetSection("SmtpUser").Value;
                emailSettings.EmailAdress = emailSettingsConfig.GetSection("EmailAdress").Value;
                emailSettings.EmailFrom = emailSettingsConfig.GetSection("EmailFrom").Value;
            }
            else
            {
                throw new Exception("EmailSettings não configurado no appsettings.json");
            }

            return emailSettings;
        }
    }
}
