namespace Events.Domain.Exceptions;

public class TicketsNotFoundException : KeyNotFoundException
{
    public TicketsNotFoundException() : base("Nema tiketa u bazi podataka.")
    {
        
    }
}