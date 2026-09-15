namespace Events.Api.Application.Services;

public interface ICurrentUserService
{
    Guid UserId {get;}
    bool IsAdmin {get;}
}