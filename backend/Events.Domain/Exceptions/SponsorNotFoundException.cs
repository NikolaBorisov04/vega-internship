namespace Events.Domain.Exceptions;

public class SponsorNotFoundException : KeyNotFoundException
{
    public SponsorNotFoundException(Guid sponsorId)
        : base($"Sponzor sa ID-jem {sponsorId} nije pronadjen.")
    {
        
    }
}