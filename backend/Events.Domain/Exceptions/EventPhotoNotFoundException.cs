namespace Events.Domain.Exceptions;

public class EventPhotoNotFoundException : KeyNotFoundException
{
    public EventPhotoNotFoundException(Guid id) : base($"Slika sa ID-jem {id} nije pronadjena.")
    {
    }
}
