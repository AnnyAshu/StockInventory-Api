using Inventory.Api.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Inventory.Api.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _log;
        private readonly IConfiguration _config;

        public EmailService(ILogger<EmailService> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        private async Task Send(string[] toEmailId, string subject, string mailBody, string[]? bcc, List<Attachment>? attachments, string emailAddressFrom = "")
        {
            try
            {
                var fromEmail = string.IsNullOrEmpty(emailAddressFrom)
                    ? _config["GmailEmailSetting:FromEmailId"]
                    : emailAddressFrom;

                var fromPassword = _config["GmailEmailSetting:FromPassword"]; // Gmail App Password

                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);
                    smtp.EnableSsl = true;

                    using (var message = new MailMessage())
                    {
                        message.From = new MailAddress(fromEmail, "Career Vault");

                        // To
                        foreach (var email in toEmailId.Distinct())
                            message.To.Add(new MailAddress(email));

                        // Subject + Body
                        message.Subject = subject;
                        message.Body = mailBody;
                        message.IsBodyHtml = true;

                        // BCC
                        if (bcc != null && bcc.Any())
                        {
                            foreach (var email in bcc.Distinct())
                                message.Bcc.Add(new MailAddress(email));
                        }

                        // Attachments
                        if (attachments != null && attachments.Any())
                        {
                            foreach (var att in attachments)
                                message.Attachments.Add(att);
                        }

                        await smtp.SendMailAsync(message);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error while sending email");
                throw;
            }
        }

        public async Task SendEmail(string toEmail, string subject, string body)
        {
            string[] to_emaillist = [toEmail];
            await Send(to_emaillist, subject, body, null, null);
        }

        public async Task SendEmail(string[] toEmail, string subject, string body)
        {
            await Send(toEmail, subject, body, null, null);
        }
    }
}
