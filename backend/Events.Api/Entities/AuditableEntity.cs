namespace Events.Api.Entities;

public abstract class AuditableEntity
{
    public Guid Id {get; set;}

    public DateTimeOffset CreatedAt {get; set;} = DateTimeOffset.UtcNow;
    
    public DateTimeOffset ModifiedAt {get; set;} = DateTimeOffset.UtcNow;
}