using Events.Application.DTOs;
using Events.Application.Messaging;

namespace Events.Application.Commands;

public sealed record CreateEventCommand(
    string Title,
    string Description,
    string Country,
    string City,
    string Address,
    string MainImageURL,
    string? VenueName,
    DateTimeOffset StartOfEvent,
    DateTimeOffset EndOfEvent
) : ICommand<EventResponseDTO>;