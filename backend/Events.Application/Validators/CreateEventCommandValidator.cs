using Events.Application.Commands;
using FluentValidation;

namespace Events.Application.Validators;

public sealed class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(x => x.dto.Title)
            .NotEmpty()
            .WithMessage("Naslov je obavezan.")
            .MaximumLength(20)
            .WithMessage("Naslov mora da ima manje od 20 karaktera.");

        RuleFor(x => x.dto.Description)
            .MaximumLength(200)
            .WithMessage("Opis mora imati manje od 200 karaktera.");

        RuleFor(x => x.dto.Country)
            .NotEmpty()
            .WithMessage("Drzava je obavezna.");

        RuleFor(x => x.dto.City)
            .NotEmpty()
            .WithMessage("Grad je obavezan.");

        RuleFor(x => x.dto.Address)
            .NotEmpty()
            .WithMessage("Adresa je obavezna.");

        RuleFor(x => x.dto.VenueName)
            .MaximumLength(20)
            .WithMessage("Ime objekta mora imati manje od 20 karaktera.");

        RuleFor(x => x.dto.StartOfEvent)
            .NotEmpty()
            .WithMessage("Datum i vreme pocetka dogadjaja su obavezni.");

        RuleFor(x => x.dto.EndOfEvent)
            .NotEmpty()
            .WithMessage("Datum i vreme zavrsetka dogadjaja su obavezni.");

        RuleFor(x => x.dto)
            .Must(x => x.EndOfEvent > x.StartOfEvent)
            .WithMessage("Kraj dogadjaja mora biti nakon pocetka.");
    }
}