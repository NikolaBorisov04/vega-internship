namespace Events.Api.Exceptions;

public class TicketTypeNotFoundException : KeyNotFoundException
{
    public TicketTypeNotFoundException(Guid ticketTypeId)
        : base($"Tip tiketa sa ID-jem {ticketTypeId} nije pronadjen.")
    {
        
    }
}