using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace SWP_SchoolMedicalManagementSystem_Service.Extension
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _smtpConfig;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpConfig = _configuration.GetSection("SMTP");
        }

        public async Task SendEmailAsync(string recipient, string body, string subject)
        {
            var _smtpServer = _smtpConfig["SmtpServer"];
            var _port = int.Parse(_smtpConfig["Port"]!);
            var _senderEmail = _smtpConfig["SenderEmail"];
            var _password = _smtpConfig["Password"];

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var client = new SmtpClient(_smtpServer, _port)
            {
                Port = _port,
                Credentials = new NetworkCredential(_senderEmail, _password),
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            })
            using (var mail = new MailMessage(_senderEmail!, recipient)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                try
                {
                    await client.SendMailAsync(mail);
                    Console.WriteLine("Email sent successfully.");
                }
                catch (SmtpException ex)
                {
                    throw new Exception($"Failed to send email: {ex.Message}", ex);
                }
            }
        }
    }
}
