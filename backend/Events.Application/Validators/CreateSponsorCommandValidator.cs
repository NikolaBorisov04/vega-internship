using Events.Application.Commands;
using FluentValidation;

namespace Events.Application.Validators;

public sealed class CreateSponsorCommandValidator : AbstractValidator<CreateSponsorCommand>
{
    public CreateSponsorCommandValidator()
    {
        RuleFor(x => x.Dto.Name)
            .NotEmpty().WithMessage("Ime je obavezno.")
            .MaximumLength(20).WithMessage("Ime ne moze imati vise od 20 karaktera.");
        RuleFor(x => x.Dto.ContactEmail)
            .NotEmpty().WithMessage("Email adresa je obavezna.")
            .EmailAddress().WithMessage("Email adresa nije validna.");
        RuleFor(x => x.Dto.Description)
            .NotEmpty().WithMessage("Opis sponzora je obavezan.");
    }
}