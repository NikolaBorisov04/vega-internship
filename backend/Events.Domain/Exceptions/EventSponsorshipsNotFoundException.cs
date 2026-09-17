namespace Events.Domain.Exceptions;

public class EventSponsorshipsNotFoundException : KeyNotFoundException
{
    public EventSponsorshipsNotFoundException() : base("Nema sponzorstva u bazi.")
    {
    }
}
