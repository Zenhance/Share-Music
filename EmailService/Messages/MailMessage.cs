using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Messages
{
    public abstract class MailMessage
    {
        public static MailMessage GetMail(string type, IEnumerable<string> to, string subject, string content, IFormFileCollection attachments)
        {
            if(type == "MailKit")
                return new MailKitMessage(to, subject, content, attachments);
            else
                return new SendGridMessage(to, subject, content, attachments);
        }
    }
}
