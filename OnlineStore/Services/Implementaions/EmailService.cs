using Microsoft.Extensions.Options;
using MimeKit;
using Mail = MailKit.Net.Smtp;
using OnlineStore.Helpers;

namespace OnlineStore.Services;

public class EmailService : IEmailService
{
    private readonly EmailSetting _settings;

    public EmailService(IOptions<EmailSetting> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = body
        };

        var smtp = new Mail.SmtpClient();
        await smtp.ConnectAsync(_settings.SmtpServer, _settings.Port, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_settings.Username, _settings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}