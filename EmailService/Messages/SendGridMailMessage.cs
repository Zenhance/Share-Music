using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;

namespace EmailService.Messages
{
    public class SendGridMailMessage
    {
        public List<EmailAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public IFormFileCollection Attachments { get; set; }

        public SendGridMailMessage(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments)
        {
            To = new List<EmailAddress>();
            To.AddRange(to.Select(x => new EmailAddress(x)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }

        public void createMessage()
        {
            throw new NotImplementedException();
        }
    }
}
