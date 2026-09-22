using Events.Domain.Entities;
using Events.Application.Commands;

namespace Events.Application.Factories;

public static class SponsorFactory
{
    public static Sponsor Create(CreateSponsorCommand command)
    {
        return new Sponsor
        {
            Name = command.dto.Name,
            ContactEmail = command.dto.ContactEmail,
            Description = command.dto.Description,
            WebsiteUrl = command.dto.WebsiteUrl,
            LogoUrl = command.dto.LogoUrl,
            TaxId = command.dto.TaxId
        };
    }
}