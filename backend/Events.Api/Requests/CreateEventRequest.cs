namespace Events.API.Requests;

public sealed class CreateEventRequest
{
    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? VenueName { get; set; }

    public DateTimeOffset StartOfEvent { get; set; }

    public DateTimeOffset EndOfEvent { get; set; }

    public IFormFile MainImage { get; set; } = null!;
}