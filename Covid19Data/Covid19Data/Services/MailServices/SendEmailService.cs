using Covid19Data.Domain.Repositories;
using Covid19Data.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Covid19Data.Services.MailServices
{
    public class SendEmailService : ISendEmailService
    {
        #region Parameters
        private readonly IConfiguration _configuration;
        private readonly ILogger<SendEmailService> _logger;
        private readonly IDestinationEmailRepository _mailRepository;
        #endregion

        #region Constructors
        public SendEmailService(ILogger<SendEmailService> logger, IConfiguration configuration, IDestinationEmailRepository mailRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _mailRepository = mailRepository;
        }
        #endregion

        #region Methods
        public async Task SendEmail(byte[] byteFile)
        {
            string emailOrigin = _configuration.GetSection("Email:origin").Value;
            string emailSubject = _configuration.GetSection("Email:subject").Value;
            string emailBody = _configuration.GetSection("Email:body").Value;
            string emailPassword = _configuration.GetSection("Email:password").Value;
            string emailHost = _configuration.GetSection("Email:host").Value;
            string date = DateTime.UtcNow.ToString("ddMMyyyy");

            try
            {
                using var message = new MailMessage
                {
                    From = new MailAddress(emailOrigin),
                    Subject = $"{emailSubject} - {date}",
                    Body = emailBody
                };

                var emails = await _mailRepository.GetEmails();

                if (emails == null || emails.Count == 0)
                    return;
                else
                    foreach (var email in emails)
                        message.To.Add(email.Email);

                using var fileStream = new MemoryStream(byteFile);
                message.Attachments.Add(new Attachment(fileStream, $"relatorioCovid19_{date}.xlsx"));

                using var smtpClient = new SmtpClient
                {
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(emailOrigin, emailPassword),

                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Host = emailHost,
                    EnableSsl = true,
                    Port = 587,
                    Timeout = 10_000
                };

                await smtpClient.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Não foi possível enviar o e-mail!");
                throw;
            }
        }
        #endregion
    }
}
