using Events.Application.DTOs;
using Events.Domain.Entities;
using Events.Application.Mappers;
using Events.Application.Repositories;

namespace Events.Application.Services;

public class EventPhotoService : IEventPhotoService
{
    private readonly IEventPhotoRepository _eventphotoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;

    public EventPhotoService(IEventPhotoRepository eventphotoRepository, IUnitOfWork unitOfWork, ResponseMapper responseMapper)
    {
        _eventphotoRepository = eventphotoRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
    }

    public async Task<EventPhotoResponseDTO?> GetByIdAsync(Guid id)
    {
        var eventphoto = await _eventphotoRepository.GetByIdAsync(id);

        if(eventphoto == null)
        {
            return null;
        }

        return _responseMapper.MapToResponse(eventphoto);
    }

    public async Task<IEnumerable<EventPhotoResponseDTO>> GetAllAsync()
    {
        var eventphotos =
            await _eventphotoRepository.GetAllAsync();

        return eventphotos.Select(_responseMapper.MapToResponse).ToList();
    }

    public async Task<EventPhotoResponseDTO> CreateAsync(EventPhotoCreateDTO dto, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(dto.Url))
        {
            throw new ArgumentException("Url slike je obavezan.");
        }

        var eventphoto = new EventPhoto
        {
            Url = dto.Url,
            Caption = dto.Caption,
            EventId = dto.EventId
        };

        _eventphotoRepository.Add(eventphoto);

        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(eventphoto);
    }
}