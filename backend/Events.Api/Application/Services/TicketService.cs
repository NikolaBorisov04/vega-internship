using Events.Api.DTOs;
using Events.Api.Entities;
using Events.Api.Mappings;
using Events.Api.IRepositories;

namespace Events.Api.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;

    public TicketService(ITicketRepository ticketRepository, IUnitOfWork unitOfWork, ResponseMapper responseMapper)
    {
        _ticketRepository = ticketRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
    }

    public async Task<TicketResponseDTO?> GetByIdAsync(Guid id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if(ticket == null)
        {
            return null;
        }

        return _responseMapper.MapToResponse(ticket);
    }

    public async Task<IEnumerable<TicketResponseDTO>> GetAllAsync()
    {
        var tickets = await _ticketRepository.GetAllAsync();

        return tickets.Select(_responseMapper.MapToResponse).ToList();
    }

    public async Task<TicketResponseDTO> CreateAsync(TicketCreateDTO dto, Guid customerId, CancellationToken ct = default)
    {
        var ticketTypeExists = await _ticketRepository.TicketTypeExistsAsync(dto.TicketTypeId, ct);

        if (!ticketTypeExists)
        {
            throw new KeyNotFoundException($"Tip tiketa sa ID-jem '{dto.TicketTypeId}' ne postoji.");
        }

        var customerExists = await _ticketRepository.CustomerExistsAsync(customerId, ct);

        if (!customerExists)
        {
            throw new KeyNotFoundException($"Kupac sa ID-jem '{customerId}' ne postoji.");
        }

        var ticket = new Ticket
        {
            QRCodeURL = dto.QRCodeURL,
            SeatNumber = dto.SeatNumber,
            IsUsed = dto.IsUsed,
            UsedAt = dto.UsedAt,
            TicketTypeId = dto.TicketTypeId,
            CustomerId = customerId
        };

        _ticketRepository.Add(ticket);

        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(ticket);
    }
}