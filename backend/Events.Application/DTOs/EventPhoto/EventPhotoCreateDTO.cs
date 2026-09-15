namespace Events.Application.DTOs;

public sealed record EventPhotoCreateDTO(
    string Url,
    string? Caption,
    Guid EventId
);