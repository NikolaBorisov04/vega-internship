using Events.Application.DTOs;

namespace Events.Application.Services;

public interface IEmailService
{
    Task SendEmailAsync(
        EmailMessageDTO messageDTO,
        CancellationToken ct = default);
}