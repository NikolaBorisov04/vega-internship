using Events.Application.Commands;
using FluentValidation;

namespace Events.Application.Validators;

public sealed class CreateSponsorCommandValidator : AbstractValidator<CreateSponsorCommand>
{
    public CreateSponsorCommandValidator()
    {
        RuleFor(x => x.dto.Name)
            .NotEmpty().WithMessage("Ime je obavezno.")
            .MaximumLength(20).WithMessage("Ime ne moze imati vise od 20 karaktera.");
        RuleFor(x => x.dto.ContactEmail)
            .NotEmpty().WithMessage("Email adresa je obavezna.")
            .EmailAddress().WithMessage("Email adresa nije validna.");
        RuleFor(x => x.dto.Description)
            .NotEmpty().WithMessage("Opis sponzora je obavezan.");
    }
}