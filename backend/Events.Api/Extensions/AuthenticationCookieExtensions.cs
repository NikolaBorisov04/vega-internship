using Events.Application.Constants;

namespace Events.Api.Extensions;

public static class AuthenticationCookieExtensions
{
    public static void SetAccessTokenCookie(
        this HttpResponse response,
        string token)
    {
        response.Cookies.Append(
            AuthenticationConstants.AccessTokenCookieName,
            token,
            CreateOptions());
    }

    public static void DeleteAccessTokenCookie(
        this HttpResponse response)
    {
        response.Cookies.Delete(
            AuthenticationConstants.AccessTokenCookieName,
            CreateOptions());
    }

    private static CookieOptions CreateOptions()
    {
        return new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            // dok sam na localhost ovo pije vodu
            SameSite = SameSiteMode.Lax,
            Path = "/",
            MaxAge = TimeSpan.FromHours(
                AuthenticationConstants.AccessTokenLifetimeHours)
        };
    }
}