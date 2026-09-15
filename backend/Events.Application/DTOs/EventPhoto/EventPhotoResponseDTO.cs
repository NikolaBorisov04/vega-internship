namespace Events.Application.DTOs;

public sealed record EventPhotoResponseDTO(
    Guid Id,
    string Url,
    string? Caption,
    Guid EventId,
    DateTimeOffset CreatedAt,
    DateTimeOffset ModifiedAt
);