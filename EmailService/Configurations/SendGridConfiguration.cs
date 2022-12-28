namespace EmailService.Configurations
{
    public class SendGridConfiguration
    {
        public string? ApiKey { get; set; }
        public string? SenderMail { get; set; }
        public string? SenderName { get; set; }
    }
}