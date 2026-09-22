using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public sealed record EventPhotoResponseDTO
(
    Guid Id,

    [property: Required]
    string Url,

    [property: Required]
    string PublicId,

    string? Caption,

    Guid EventId,

    DateTimeOffset CreatedAt,

    DateTimeOffset ModifiedAt
);