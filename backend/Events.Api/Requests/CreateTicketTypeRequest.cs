using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Events.API.Requests;

public sealed record CreateTicketTypeRequest
{
    [BindRequired, Required]
    public required string Name { get; init; }

    [BindRequired, Required]
    public required decimal Price { get; init; }

    [BindRequired, Required]
    public required Guid EventId {get; init;}

    [BindRequired, Required]
    public required string Description {get; init;}

    [BindRequired, Required]
    public required int QuantityAvailable {get; init;}

    [BindRequired, Required]
    public required IFormFile BackgroundImage { get; init; }
}