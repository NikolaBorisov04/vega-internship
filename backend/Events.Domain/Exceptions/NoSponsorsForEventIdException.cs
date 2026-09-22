namespace Events.Domain.Exceptions;

public class NoSponsorsForEventIdException : KeyNotFoundException
{
    public NoSponsorsForEventIdException(Guid eventId)
        : base($"Dogadjaj sa ID-jem {eventId} nema sponzore.")
    {
        
    }
}