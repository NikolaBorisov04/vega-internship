namespace Events.Domain.Exceptions;

public class TicketNotFoundException : KeyNotFoundException
{
    public TicketNotFoundException(Guid ticketId)
        : base($"Tiketa sa ID-jem {ticketId} nije pronadjen.")
    {
        
    }
}