using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Events.API.Requests;

public sealed record CreateEventPhotoRequest
{
    public string? Caption { get; init; }

    [BindRequired, Required]
    public required Guid EventId {get; init;}

    [BindRequired, Required]
    public required IFormFile Image { get; init; }
}