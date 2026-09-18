using Events.Domain.Entities;
using Events.Application.Commands;

namespace Events.Application.Factories;

public static class EventSponsorshipFactory
{
    public static EventSponsorship Create(CreateEventSponsorshipCommand command)
    {
        return new EventSponsorship
        {
            ContributionAmount = command.dto.ContributionAmount,
            EventId = command.dto.EventId,
            SponsorId = command.dto.SponsorId
        };
    }
}