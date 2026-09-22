namespace Events.Domain.Exceptions;

public class NoEventPhotosForEventIdException : KeyNotFoundException
{
    public NoEventPhotosForEventIdException(Guid eventId)
        : base($"Dogadjaj sa ID-jem {eventId} nema slike.")
    {
        
    }
}