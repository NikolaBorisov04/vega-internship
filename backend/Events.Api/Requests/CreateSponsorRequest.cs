using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Events.API.Requests;

public sealed record CreateSponsorRequest
{
    [BindRequired, Required]
    public required string Name { get; init; }

    [BindRequired, Required]
    public required string ContactEmail { get; init; }

    [BindRequired, Required]
    public required string Description {get; init;}

    [BindRequired, Required]
    public required string WebsiteUrl {get; init;}

    [BindRequired, Required]
    public required string TaxId { get; init; }

    [BindRequired, Required]
    public required IFormFile BackgroundImage { get; init; }
}