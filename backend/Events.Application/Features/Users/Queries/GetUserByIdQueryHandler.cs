using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Queries;
public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponseDTO>
{
    private readonly ResponseMapper _responseMapper;
    private readonly IUserRepository _userRepository;
    public GetUserByIdQueryHandler(
        ResponseMapper responseMapper,
        IUserRepository userRepository
    )
    {
        _responseMapper = responseMapper;
        _userRepository = userRepository;
    }

    public async Task<UserResponseDTO> Handle(GetUserByIdQuery query, CancellationToken ct = default)
    {
        var result = await _userRepository.GetByIdAsync(query.Id) ?? throw new UserNotFoundException(query.Id);
        return _responseMapper.MapToResponse(result);
    }
}