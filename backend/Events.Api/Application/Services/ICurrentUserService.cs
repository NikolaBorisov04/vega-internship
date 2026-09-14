namespace Events.Api.Services;

public interface ICurrentUserService
{
    Guid UserId {get;}
    bool IsAdmin {get;}
}