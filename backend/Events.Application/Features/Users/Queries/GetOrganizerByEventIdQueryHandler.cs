using Events.Application.DTOs;
using Events.Application.Repositories;
using Events.Application.Mappers;
using MediatR;
using Events.Domain.Exceptions;

namespace Events.Application.Queries;

public sealed class GetOrganizerByEventIdQueryHandler : IRequestHandler<GetOrganizerByEventIdQuery, UserResponseDTO>
{
    private readonly IOrganizerRepository _organizerRepository;
    private readonly ResponseMapper _responseMapper;

    public GetOrganizerByEventIdQueryHandler(
        IOrganizerRepository organizerRepository,
        ResponseMapper responseMapper)
    {
        _organizerRepository = organizerRepository;
        _responseMapper = responseMapper;
    }

    public async Task<UserResponseDTO> Handle(
        GetOrganizerByEventIdQuery query,
        CancellationToken ct)
    {
        var organizer = await _organizerRepository.GetByEventIdAsync(
            query.EventId,
            ct);

        if (organizer is null)
            throw new NoOrganizerForEventIdException(query.EventId);

        return _responseMapper.MapToResponse(organizer);
    }
}