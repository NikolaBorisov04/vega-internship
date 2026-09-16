using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Queries;

public sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserResponseDTO>>
{
    private readonly IUserRepository _userRepository;
    private readonly ResponseMapper _responseMapper;
    public GetUsersQueryHandler(IUserRepository userRepository, ResponseMapper responseMapper)
    {
        _userRepository = userRepository;
        _responseMapper = responseMapper;
    }
    public async Task<List<UserResponseDTO>> Handle(GetUsersQuery query, CancellationToken ct = default)
    {
        var users = await _userRepository.GetAllAsync(ct) ?? throw new UsersNotFoundException();
        return users.Select(_responseMapper.MapToResponse).ToList();
    }
}