namespace Events.Domain.Exceptions;

public class EventPhotosNotFoundException : KeyNotFoundException
{
    public EventPhotosNotFoundException() : base("Nema slika dogadjaja u bazi.")
    {
    }
}
