using Events.Application.DTOs;
using Events.Application.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Events.Infrastructure.Email;

public sealed class GmailEmailService : IEmailService
{
    private readonly EmailOptions _options;
    private readonly ILogger<GmailEmailService> _logger;

    public GmailEmailService(
        IOptions<EmailOptions> options,
        ILogger<GmailEmailService> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(EmailMessageDTO email, CancellationToken ct = default)
    {
        try
        {
            using var client = new SmtpClient();

            await client.ConnectAsync(
                _options.SmtpHost,
                _options.SmtpPort,
                SecureSocketOptions.StartTls,
                ct);

            await client.AuthenticateAsync(
                _options.Email,
                _options.AppPassword,
                ct);

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_options.DisplayName, _options.Email));

            message.To.Add(new MailboxAddress(email.RecipientName ?? email.RecipientEmail, email.RecipientEmail));

            message.Subject = email.Subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = email.HtmlContent
            };

            message.Body = bodyBuilder.ToMessageBody();

            await client.SendAsync(message, ct);

            await client.DisconnectAsync(true, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Mejl nije uspesno poslat na {RecipientEmail}.", email.RecipientEmail);

            throw;
        }
    }
}