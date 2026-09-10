using Microsoft.EntityFrameworkCore;
using Events.Api.Data;
using Events.Api.DTOs;
using Events.Api.Entities;
using Events.Api.Extensions;
using Events.Api.Mappings;

namespace Events.Api.Services;
public class SponsorService : ISponsorService
{
    private readonly ApplicationDbContext _context;
    private readonly ResponseMapper _responseMapper;

    public SponsorService(ApplicationDbContext context, ResponseMapper responseMapper)
    {
        _context = context;
        _responseMapper = responseMapper;
    }

    public async Task<SponsorResponseDTO?> GetByIdAsync(Guid id)
    {
        return await _context.Sponsors
            .Where(s => s.Id == id)
            .ToSponsorResponseDTO()
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<SponsorResponseDTO>> GetAllAsync()
    {
        return await _context.Sponsors
            .ToSponsorResponseDTO()
            .ToListAsync();
    }

    public async Task<SponsorResponseDTO> CreateAsync(SponsorCreateDTO dto, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new ArgumentException("Naziv sponzora je obavezan.");
        }

        if (string.IsNullOrWhiteSpace(dto.ContactEmail))
        {
            throw new ArgumentException("Email sponzora je obavezan.");
        }

        if (string.IsNullOrWhiteSpace(dto.Description))
        {
            throw new ArgumentException("Opis sponzora je obavezan.");
        }

        if (string.IsNullOrWhiteSpace(dto.LogoUrl))
        {
            throw new ArgumentException("Logo sponzora je obavezan.");
        }

        var sponsor = new Sponsor
        {
            Name = dto.Name,
            ContactEmail = dto.ContactEmail,
            Description = dto.Description,
            LogoUrl = dto.LogoUrl,
            TaxId = dto.TaxId,
            WebsiteUrl = dto.WebsiteUrl
        };

        _context.Sponsors.Add(sponsor);
        await _context.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(sponsor);
    }
}