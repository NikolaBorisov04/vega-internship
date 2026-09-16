using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Queries;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<UserResponseDTO>;