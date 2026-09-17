using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public sealed record EventPhotoCreateDTO
(
    [property: Required]
    string Url,

    string? Caption,

    Guid EventId
);