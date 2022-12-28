using EmailService.Messages;

namespace EmailService.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MailMessage message);
    }
}