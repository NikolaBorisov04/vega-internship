using Events.Application.Repositories;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Commands;

public sealed class DeleteSponsorCommandHandler : IRequestHandler<DeleteSponsorCommand, string>
{
    private readonly ISponsorRepository _sponsorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSponsorCommandHandler(
        ISponsorRepository sponsorRepository,
        IUnitOfWork unitOfWork)
    {
        _sponsorRepository = sponsorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(
        DeleteSponsorCommand command,
        CancellationToken ct)
    {
        var sponsor = await _sponsorRepository.GetByIdAsync(command.Id, ct) ?? throw new SponsorNotFoundException(command.Id);
        _sponsorRepository.Delete(sponsor);

        await _unitOfWork.SaveChangesAsync(ct);

        return $"Korisnik sa ID-jem {command.Id} je uspeno izbrisan.";
    }
}