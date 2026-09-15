namespace Events.Domain.Exceptions;

public class EventNotFoundException : KeyNotFoundException
{
    public EventNotFoundException(Guid eventId)
        : base($"Dogadjaj sa ID-jem {eventId} nije pronadjen.")
    {
        
    }
}