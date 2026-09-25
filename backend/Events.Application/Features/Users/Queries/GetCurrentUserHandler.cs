using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Application.Services;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Queries;

public sealed class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserResponseDTO>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly ResponseMapper _responseMapper;

    public GetCurrentUserQueryHandler(
        IUserRepository userRepository,
        ICurrentUserService currentUserService,
        ResponseMapper responseMapper)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
        _responseMapper = responseMapper;
    }

    public async Task<UserResponseDTO> Handle(GetCurrentUserQuery request, CancellationToken ct)
    {
        var userId = _currentUserService.UserId;

        var user = await _userRepository.GetByIdAsync(userId, ct);

        if (user is null)
        {
            throw new UserNotFoundException(userId);
        }

        return _responseMapper.MapToResponse(user);
    }
}