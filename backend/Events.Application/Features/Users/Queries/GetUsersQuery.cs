using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Queries;
public sealed record GetUsersQuery : IRequest<List<UserResponseDTO>>;