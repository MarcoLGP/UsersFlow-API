using System.Net;
using System.Net.Mail;

namespace UsersFlow_API.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _password;
        private readonly string _sender;

        public EmailService(IConfiguration _config)
        {
             IConfigurationSection emailSection = _config.GetSection("email");
            _sender = emailSection.GetValue<string>("sender") ?? throw new Exception("Invalid sender email");
            _host = emailSection.GetValue<string>("host") ?? throw new Exception("Invalid host email");
            _port = emailSection.GetValue<int>("port");
            _password = emailSection.GetValue<string>("password") ?? throw new Exception("Invalid password email");
        }

        public async Task<bool> SendEmailAsync(string email, string subject, string content)
        {
                using var smtpClient = new SmtpClient();
                smtpClient.Host = _host;
                smtpClient.Port = _port;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_sender, _password);
                smtpClient.EnableSsl = true;

                var message = new MailMessage();
                message.From = new MailAddress(_sender);
                message.To.Add(email);
                message.Subject = subject;
                message.Body = content;

                await smtpClient.SendMailAsync(message);
                return true;
        }
    }
}
