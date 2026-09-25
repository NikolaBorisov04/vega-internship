using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Commands;

public sealed record LoginCommand(LoginDTO Dto) : IRequest<LoginResultDTO>;
