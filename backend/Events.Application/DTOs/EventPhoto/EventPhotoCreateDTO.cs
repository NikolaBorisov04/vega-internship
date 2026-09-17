using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public sealed record EventPhotoCreateDTO
(
    [param: Required]
    string Url,

    string? Caption,

    Guid EventId
);