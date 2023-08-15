using Microsoft.Extensions.Configuration;
using MTS.CommonLibrary.Email.Abstraction;
using MTS.CommonLibrary.Logger.Abstraction;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MTS.CommonLibrary.Email.Implementation
{
    public class EmailSender : IEmailSender
    {
        private readonly IMTSLogger _logger;
        private IConfiguration configuration { get; }
        public EmailSender(IMTSLogger logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            _logger.Information("Enter into method : SMSDataAccess.EmailSender.EmailSender.SendEmailAsync");

            String FROM = configuration["Keys:MailFrom"];
            String FROMNAME = configuration["Keys:MailFromName"];
            String SMTP_USERNAME = configuration["Keys:SMTPUserName"];
            String SMTP_PASSWORD = configuration["Keys:SMTPPassword"];
            String HOST = configuration["Keys:SMTPHost"];
            String TO = email;
            String SUBJECT = subject;
            String BODY = message;
            int PORT = 587;

            // Create and build a new MailMessage object
            MailMessage mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.From = new MailAddress(FROM, FROMNAME);
            mailMessage.To.Add(new MailAddress(TO));
            mailMessage.Subject = SUBJECT;
            mailMessage.Body = BODY;

            SmtpClient client = new SmtpClient(HOST, PORT);

            client.Credentials = new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);

            client.EnableSsl = true;

            try
            {
                //Console.WriteLine("Attempting to send email...");
                client.SendAsync(mailMessage, null);
                //_logger.LogInformation("Exit from method : SMSDataAccess.EmailSender.EmailSender.SendEmailAsync");
                //Console.WriteLine("Email sent!");
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, ex.Message);
            }
        }
    }

    public class EmailSenderV2 : IEmailSender
    {
        private readonly IMTSLogger _logger;
        private IConfiguration configuration { get; }
        public EmailSenderV2(IMTSLogger logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            //_logger.LogInformation("Enter into method : SMSDataAccess.EmailSender.EmailSender.SendEmailAsync");

            String FROM = configuration["Keys:MailFrom"];
            String FROMNAME = configuration["Keys:MailFromName"];
            String SMTP_USERNAME = configuration["Keys:SMTPUserName"];
            String SMTP_PASSWORD = configuration["Keys:SMTPPassword"];
            String HOST = configuration["Keys:SMTPHost"];
            String TO = email;
            String SUBJECT = subject;
            String BODY = message;
            int PORT = 587;

            // Create and build a new MailMessage object
            MailMessage mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.From = new MailAddress(FROM, FROMNAME);
            mailMessage.To.Add(new MailAddress(TO));
            mailMessage.Subject = SUBJECT;
            mailMessage.Body = BODY;

            SmtpClient client = new SmtpClient(HOST, PORT);

            client.Credentials = new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);

            client.EnableSsl = true;

            try
            {
                //Console.WriteLine("Attempting to send email...");
                client.SendAsync(mailMessage, null);
                // _logger.LogInformation("Exit from method : SMSDataAccess.EmailSender.EmailSender.SendEmailAsync");
                //Console.WriteLine("Email sent!");
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, ex.Message);
            }
        }
    }
}
