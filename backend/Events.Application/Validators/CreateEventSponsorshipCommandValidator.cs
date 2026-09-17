using Events.Application.Commands;
using FluentValidation;

namespace Events.Application.Validators;

public sealed class CreateEventSponsorshipCommandValidator : AbstractValidator<CreateEventSponsorshipCommand>
{
    public CreateEventSponsorshipCommandValidator()
    {
        RuleFor(x => x.dto.ContributionAmount)
            .GreaterThan(0).WithMessage("Iznos donacije mora biti veci od 0.");
        RuleFor(x => x.dto.EventId)
            .NotEmpty().WithMessage("ID dogadjaja je obavezan.");
        RuleFor(x => x.dto.SponsorId)
            .NotEmpty().WithMessage("ID sponzora je obavezan.");
    }
}
