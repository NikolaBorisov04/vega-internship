using Microsoft.EntityFrameworkCore;
using Events.Api.Data;
using Events.Api.DTOs;
using Events.Api.Entities;
using Events.Api.Extensions;

namespace Events.Api.Services;
public class TicketService : ITicketService
{
    private readonly ApplicationDbContext _context;

    public TicketService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TicketResponseDTO?> GetByIdAsync(Guid id)
    {
        return await _context.Tickets
            .Where(t => t.Id == id)
            .ToTicketResponseDTO()
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TicketResponseDTO>> GetAllAsync()
    {
        return await _context.Tickets
            .ToTicketResponseDTO()
            .ToListAsync();
    }

    public async Task<TicketResponseDTO> CreateAsync(TicketCreateDTO ticketCreateDto, CancellationToken ct = default)
    {
        var ticketTypeExists = await _context.TicketTypes
            .AnyAsync(t => t.Id == ticketCreateDto.TicketTypeId, ct);

        if (!ticketTypeExists)
        {
            throw new KeyNotFoundException(
                $"Tip tiketa sa ID-jem '{ticketCreateDto.TicketTypeId}' ne postoji.");
        }

        var customerExists = await _context.Customers
            .AnyAsync(c => c.Id == ticketCreateDto.CustomerId, ct);

        if (!customerExists)
        {
            throw new KeyNotFoundException(
                $"Kupac sa ID-jem '{ticketCreateDto.CustomerId}' ne postoji.");
        }

        var ticket = new Ticket
        {
            QRCodeURL = ticketCreateDto.QRCodeURL,
            SeatNumber = ticketCreateDto.SeatNumber,
            IsUsed = ticketCreateDto.IsUsed,
            UsedAt = ticketCreateDto.UsedAt,
            TicketTypeId = ticketCreateDto.TicketTypeId,
            CustomerId = ticketCreateDto.CustomerId
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync(ct);

        return MapToResponse(ticket);
    }
    private static TicketResponseDTO MapToResponse(Ticket ticket)
    {
        return new TicketResponseDTO(
            ticket.Id,
            ticket.TicketCode,
            ticket.QRCodeURL,
            ticket.SeatNumber,
            ticket.IsUsed,
            ticket.UsedAt,
            ticket.TicketTypeId,
            ticket.CustomerId
        );
    }
}