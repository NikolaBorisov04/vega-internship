using Events.Api.DTOs;
using Events.Api.Entities;
using Events.Api.Mappings;
using Events.Api.IRepositories;

namespace Events.Api.Services;

public class SponsorService : ISponsorService
{
    private readonly ISponsorRepository _sponsorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;

    public SponsorService(ISponsorRepository sponsorRepository, IUnitOfWork unitOfWork, ResponseMapper responseMapper)
    {
        _sponsorRepository = sponsorRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
    }

    public async Task<SponsorResponseDTO?> GetByIdAsync(Guid id)
    {
        var sponsor = await _sponsorRepository.GetByIdAsync(id);

        if(sponsor == null)
        {
            return null;
        }

        return _responseMapper.MapToResponse(sponsor);
    }

    public async Task<IEnumerable<SponsorResponseDTO>> GetAllAsync()
    {
        var sponsors =
            await _sponsorRepository.GetAllAsync();

        return sponsors.Select(_responseMapper.MapToResponse).ToList();
    }

    public async Task<SponsorResponseDTO> CreateAsync(SponsorCreateDTO dto, CancellationToken ct = default)
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

        _sponsorRepository.Add(sponsor);

        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(sponsor);
    }
}