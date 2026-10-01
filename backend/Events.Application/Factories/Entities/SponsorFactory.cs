using Events.Domain.Entities;
using Events.Application.Commands;

namespace Events.Application.Factories;

public static class SponsorFactory
{
    public static Sponsor Create(CreateSponsorCommand command, string imageUrl, string imagePublicId)
    {
        return new Sponsor
        {
            Name = command.Dto.Name,
            ContactEmail = command.Dto.ContactEmail,
            Description = command.Dto.Description,
            WebsiteUrl = command.Dto.WebsiteUrl,
            ImageUrl = imageUrl,
            ImagePublicId = imagePublicId,
            TaxId = command.Dto.TaxId
        };
    }
}