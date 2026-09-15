namespace Events.Application.Services;

public interface ICurrentUserService
{
    Guid UserId {get;}
    bool IsAdmin {get;}
}