namespace Events.Domain.Exceptions;

public class EventsNotFoundException : KeyNotFoundException
{
    public EventsNotFoundException()
        : base("Nema dogadjaja u bazi podataka")
    {
        
    }
}