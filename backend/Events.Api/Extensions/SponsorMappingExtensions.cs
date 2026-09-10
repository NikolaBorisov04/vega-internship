using Events.Api.DTOs;
using Events.Api.Entities;

namespace Events.Api.Extensions;
public static class SponsorMappingExtensions
{
    public static IQueryable<SponsorResponseDTO> ToSponsorResponseDTO(this IQueryable<Sponsor> query)
    {
        return query.Select(s => new SponsorResponseDTO(
            s.Id,
            s.Name,
            s.ContactEmail,
            s.Description,
            s.LogoUrl,
            s.WebsiteUrl
        ));
    }
}