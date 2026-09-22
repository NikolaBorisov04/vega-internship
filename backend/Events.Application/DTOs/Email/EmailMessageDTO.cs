namespace Events.Application.DTOs;

public sealed record EmailMessageDTO(
    string RecipientEmail,
    string Subject,
    string HtmlContent,
    string? RecipientName = null
);