namespace Events.Domain.Exceptions;

public class NoTicketTypesForEventIdException : KeyNotFoundException
{
    public NoTicketTypesForEventIdException(Guid eventId)
        : base($"Dogadjaj sa ID-jem {eventId} nema tipove tiketa.")
    {
        
    }
}