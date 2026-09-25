using Events.Application.DTOs;
using Events.Application.Storage;
using MediatR;

namespace Events.Application.Commands;

public sealed record CreateTicketTypeCommand(TicketTypeCreateDTO Dto, FileUpload Image) : IRequest<TicketTypeResponseDTO>;
