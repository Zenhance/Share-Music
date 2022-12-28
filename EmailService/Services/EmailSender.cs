using SendGrid;
using SendGrid.Helpers.Mail;
using EmailService.Configurations;
using EmailService.Messages;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Net;

namespace EmailService.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailMailKitConfiguration config;

        public EmailSender(EmailMailKitConfiguration config)
        {
            this.config = config;
        }

        public async Task SendEmailAsync(MailKitMessage message)
        {
            var mailMessage = CreateEmailMessage(message);

            await SendAsync(mailMessage);
        }

        private MimeMessage CreateEmailMessage(MailKitMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(config.UserName,config.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<h2 style='color:red;'>{0}</h2>", message.Content) };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.ServerCertificateValidationCallback = (sender, certificate, certificateChainType, errors) => true;
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.ConnectAsync(config.SmtpServer, config.Port, SecureSocketOptions.SslOnConnect).ConfigureAwait(false);
                    await client.AuthenticateAsync(new NetworkCredential( config.From, config.Password)).ConfigureAwait(false);
                    await client.SendAsync(mailMessage).ConfigureAwait(false);
                }
                catch
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
