using Events.Api.DTOs;
using Events.Api.Entities;

namespace Events.Api.Extensions;

public static class UserMappingExtensions
{
    public static IQueryable<UserResponseDTO> ToUserResponseDTO(this IQueryable<User> query)
    {
        return query.Select(u => new UserResponseDTO(
            u.Id,
            u.Name,
            u.Email,
            u.Role,
            u is Organizer ? ((Organizer)u).CompanyName : null,
            u is Organizer ? ((Organizer)u).Validated : (bool?)null
        ));
    }
}