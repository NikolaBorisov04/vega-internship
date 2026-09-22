namespace Events.Domain.Exceptions;

public class EventSponsorshipNotFoundException : KeyNotFoundException
{
    public EventSponsorshipNotFoundException(Guid id) : base($"Sponzorstvo sa ID-jem {id} nije pronadjeno.")
    {
    }
}
