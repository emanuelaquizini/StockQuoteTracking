﻿using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using StockQuoteTracking.src.Configurations;
using StockQuoteTracking.src.Interfaces;
using StockQuoteTracking.src.Models;

namespace StockQuoteTracking.src.Services
{

    public class EmailService : IEmailService
    {

        private readonly IConfig _config;

        public EmailService(IConfig config)
        {
            _config = config;
        }

        public void SendEmail(string emailSubject, string emailBody)
        {
            try
            {

                EmailSettings emailSettings = _config.LoadEmailSettings();

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(emailSettings.EmailFrom),
                    Subject = emailSubject,
                    Body = emailBody
                };
                mailMessage.To.Add(emailSettings.EmailAdress);

                using (var smtpClient = new SmtpClient(emailSettings.SmtpServer, emailSettings.SmtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(emailSettings.SmtpUser, emailSettings.SmtpPassword);
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMessage);
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Erro no formato do endereço de e-mail: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
            }
        }


    }

}