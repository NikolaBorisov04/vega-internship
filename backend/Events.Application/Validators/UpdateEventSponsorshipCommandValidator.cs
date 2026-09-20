using Events.Application.Commands;
using FluentValidation;

namespace Events.Application.Validators;

public sealed class UpdateEventSponsorshipCommandValidator : AbstractValidator<UpdateEventSponsorshipCommand>
{
    public UpdateEventSponsorshipCommandValidator()
    {
        When(x => x.Dto.ContributionAmount.HasValue, () =>
        {
            RuleFor(x => x.Dto.ContributionAmount!.Value)
                .GreaterThan(0).WithMessage("Iznos sponzorstva mora biti veci od 0.");
        });
    }
}
