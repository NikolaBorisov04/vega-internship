using Events.Application.DTOs;
using Events.Application.Factories;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Application.Services;
using Events.Application.Storage;
using Events.Domain.Entities;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Commands;

public sealed class CreateTicketTypeCommandHandler : IRequestHandler<CreateTicketTypeCommand, TicketTypeResponseDTO>
{
    private readonly ITicketTypeRepository _ticketTypeRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly IImageStorage _imageStorage;

    public CreateTicketTypeCommandHandler(
        ITicketTypeRepository ticketTypeRepository,
        IEventRepository eventRepository,
        IUnitOfWork unitOfWork,
        ResponseMapper responseMapper,
        ICurrentUserService currentUserService,
        IImageStorage imageStorage)
    {
        _ticketTypeRepository = ticketTypeRepository;
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
        _currentUserService = currentUserService;
        _imageStorage = imageStorage;
    }

    public async Task<TicketTypeResponseDTO> Handle(CreateTicketTypeCommand command, CancellationToken ct)
    {
        var eventExists = await _ticketTypeRepository.EventExistsAsync(command.Dto.EventId, ct);
        if (!eventExists)
        {
            throw new EventNotFoundException(command.Dto.EventId);
        }

        if (!_currentUserService.IsAdmin)
        {
            var organizerId = await _eventRepository.GetEventOrganizerIdAsync(command.Dto.EventId, ct);
            if (organizerId != _currentUserService.UserId)
            {
                throw new UnauthorizedAccessException("You do not have permission to create a ticket type for this event.");
            }
        }

        var image = await _imageStorage.UploadAsync(command.Image.Stream, command.Image.FileName, ct);

        try
        {
            var newticketType = TicketTypeFactory.Create(command, image.Url, image.PublicId);

            _ticketTypeRepository.Add(newticketType);

            await _unitOfWork.SaveChangesAsync(ct);

            return _responseMapper.MapToResponse(newticketType);
        }
        catch
        {
            await _imageStorage.DeleteAsync(image.PublicId, ct);

            throw new ArgumentException("Ticket type wasn't created succesfully.");
        }
    }
}
