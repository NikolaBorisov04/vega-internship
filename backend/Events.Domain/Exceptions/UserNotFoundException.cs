namespace Events.Domain.Exceptions;

public class UserNotFoundException : KeyNotFoundException
{
    public UserNotFoundException(Guid userId)
        : base($"Korisnik sa ID-jem {userId} nije pronadjen.")
    {
        
    }
}