using Events.Application.Commands;
using FluentValidation;

namespace Events.Application.Validators;

public sealed class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
{
    public UpdateEventCommandValidator()
    {
        When(x => x.dto.Title is not null, () =>
        {
            RuleFor(x => x.dto.Title)
                .NotEmpty().WithMessage("Naslov ne moze biti prazan.")
                .MaximumLength(20).WithMessage("Naslov mora da ima manje od 20 karaktera.");
        });

        When(x => x.dto.Description is not null, () =>
        {
            RuleFor(x => x.dto.Description)
                .NotEmpty().WithMessage("Opis ne moze biti prazan.")
                .MaximumLength(200).WithMessage("Opis mora imati manje od 200 karaktera.");
        });

        When(x => x.dto.Country is not null, () =>
        {
            RuleFor(x => x.dto.Country)
                .NotEmpty().WithMessage("Drzava ne moze biti prazna.")
                .MaximumLength(100).WithMessage("Drzava ne moze imati vise od 100 karaktera.");
        });

        When(x => x.dto.City is not null, () =>
        {
            RuleFor(x => x.dto.City)
                .NotEmpty().WithMessage("Grad ne moze biti prazan.")
                .MaximumLength(100).WithMessage("Grad ne moze imati vise od 100 karaktera.");
        });

        When(x => x.dto.Address is not null, () =>
        {
            RuleFor(x => x.dto.Address)
                .NotEmpty().WithMessage("Adresa ne moze biti prazna.")
                .MaximumLength(200).WithMessage("Adresa ne moze imati vise od 200 karaktera.");
        });

        When(x => x.dto.MainImageURL is not null, () =>
        {
            RuleFor(x => x.dto.MainImageURL)
                .NotEmpty().WithMessage("URL glavne slike ne moze biti prazan.")
                .MaximumLength(1000).WithMessage("URL glavne slike ne moze imati vise od 1000 karaktera.");
        });

        When(x => x.dto.VenueName is not null, () =>
        {
            RuleFor(x => x.dto.VenueName)
                .MaximumLength(20).WithMessage("Ime objekta mora imati manje od 20 karaktera.");
        });

        When(x => x.dto.StartOfEvent.HasValue && x.dto.EndOfEvent.HasValue, () =>
        {
            RuleFor(x => x.dto.EndOfEvent!.Value)
                .GreaterThan(x => x.dto.StartOfEvent!.Value)
                .WithMessage("Datum zavrsetka mora biti nakon datuma pocetka.");
        });
    }
}
