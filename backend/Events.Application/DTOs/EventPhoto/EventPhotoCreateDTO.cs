using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public sealed record EventPhotoCreateDTO
(
    string? Caption,

    Guid EventId
);