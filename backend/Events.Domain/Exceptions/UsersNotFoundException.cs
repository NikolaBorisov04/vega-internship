namespace Events.Domain.Exceptions;

public class UsersNotFoundException : KeyNotFoundException
{
    public UsersNotFoundException() : base("Nema korisnika u bazi podataka.")
    {
        
    }
}