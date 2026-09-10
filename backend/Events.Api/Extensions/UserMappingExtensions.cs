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
            u.CreatedAt,
            u.ModifiedAt,
            u is Customer ? ((Customer)u).Country : null,
            u is Customer ? ((Customer)u).City : null,
            u is Customer ? ((Customer)u).Address : null,
            u is Customer ? ((Customer)u).PhoneNumber : null,
            u is Organizer ? ((Organizer)u).CompanyName : null,
            u is Organizer ? ((Organizer)u).Validated : (bool?)null
        ));
    }
}