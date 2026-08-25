using Microsoft.Extensions.Options;
using Nofication.Application.Interfaces;
using Notification.Infrastructure.ConfigurationsSettings;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MediatR;


namespace Notification.Infrastructure.Repositories
{
    public class EmailService : IEmailService
    {

        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task<bool> SendEmail(string recipient, string message)
        {
            var email = new MimeMessage();

            email.From.Add(
                new MailboxAddress(
                    _emailSettings.FromName,
                    _emailSettings.Username));

            email.To.Add(MailboxAddress.Parse(recipient));

            email.Subject = $"Welcome, {recipient}!";

            email.Body = new TextPart("plain")
            {
                Text = message
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _emailSettings.Host,
                _emailSettings.Port,
                SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                _emailSettings.Username,
               _emailSettings.Password);

            await smtp.SendAsync(email);

            await smtp.DisconnectAsync(true);

            return true;
        }
    }
}
