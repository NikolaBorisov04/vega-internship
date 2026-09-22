namespace Events.Domain.Exceptions;

public class SponsorsNotFoundException : KeyNotFoundException
{
    public SponsorsNotFoundException()
        : base("Nema sponzora u bazi podataka")
    {
        
    }
}