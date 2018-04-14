﻿using System;
using System.Diagnostics;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DoubleTap.Email
{
    public class SendEmailOverSmtp : IEmailClient
    {
        readonly ISmtpClientConfig _config;

        public SendEmailOverSmtp(ISmtpClientConfig config)
        {
            _config = config;
        }

        public async Task SendAsync(Email email)
        {
            var mailMessage = new MailMessage
            {
                Body       = email.Body,
                Subject    = email.Subject,
                IsBodyHtml = email.IsBodyHtml
            };

            if (email.From != null)
            {
                mailMessage.From = new MailAddress(email.From);
            }

            foreach (var to in email.To)
            {
                mailMessage.To.Add(to);
            }

            foreach (var attachment in email.Attachments)
            {
                var emailAttachment = new System.Net.Mail.Attachment(attachment.FilePath) {Name = attachment.Name};
                mailMessage.Attachments.Add(emailAttachment);
            }

            using (var smtpClient = new SmtpClient(_config.Host, _config.Port))
            {
                switch (_config.Mode)
                {
                    case SmtpClientMode.Email:
                        break;
                    case SmtpClientMode.Directory:
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        break;
                    case SmtpClientMode.Disabled:
                        Trace.TraceInformation($"Sending emails is disabled. Request to send email '{mailMessage.Subject}' to {formatToAddress(email.To)}");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
                catch (SmtpException ex)
                {
                    throw new EmailClientException($"An error occurred when sending email to {formatToAddress(email.To)}", ex);
                }
            }
        }

        string formatToAddress(string[] to)
        {
            return $"[{string.Join(", ", to)}]";
        }
    }
}