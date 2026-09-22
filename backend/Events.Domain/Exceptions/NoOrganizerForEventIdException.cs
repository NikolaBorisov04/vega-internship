namespace Events.Domain.Exceptions;

public class NoOrganizerForEventIdException : KeyNotFoundException
{
    public NoOrganizerForEventIdException(Guid eventId)
        : base($"Dogadjaj sa ID-jem {eventId} nema organizatora.")
    {
        
    }
}