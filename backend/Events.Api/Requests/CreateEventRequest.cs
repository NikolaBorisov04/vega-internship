using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Events.API.Requests;

public sealed record CreateEventRequest
{
    [BindRequired, Required]
    public required string Title { get; init; }

    [BindRequired, Required]
    public required string Description { get; init; }

    [BindRequired, Required]
    public required string Country { get; init; }

    [BindRequired, Required]
    public required string City { get; init; }

    [BindRequired, Required]
    public required string Address { get; init; }

    public string? VenueName { get; init; }

    [BindRequired, Required]
    public required DateTimeOffset StartOfEvent { get; init; }

    [BindRequired, Required]
    public required DateTimeOffset EndOfEvent { get; init; }

    [BindRequired, Required]
    public required IFormFile MainImage { get; init; }
}