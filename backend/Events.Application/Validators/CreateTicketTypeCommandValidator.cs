using Events.Application.Commands;
using FluentValidation;

namespace Events.Application.Validators;

public sealed class CreateTicketTypeCommandValidator : AbstractValidator<CreateTicketTypeCommand>
{
    public CreateTicketTypeCommandValidator()
    {
        RuleFor(x => x.dto.Name)
            .NotEmpty().WithMessage("Ime je obavezno.")
            .MaximumLength(100).WithMessage("Ime ne moze imati vise od 100 karaktera.");

        RuleFor(x => x.dto.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Cena mora biti veca ili jednaka 0.");

        RuleFor(x => x.dto.EventId)
            .NotEmpty().WithMessage("EventId je obavezan.");

        RuleFor(x => x.dto.Description)
            .NotEmpty().WithMessage("Opis tipa tiketa je obavezan.")
            .MaximumLength(500).WithMessage("Opis ne moze imati vise od 500 karaktera.");

        RuleFor(x => x.dto.QuantityAvailable)
            .GreaterThanOrEqualTo(0).WithMessage("Dostupna kolicina mora biti veca ili jednaka 0.");

        RuleFor(x => x.dto.TicketBackgroundImageUrl)
            .NotEmpty().WithMessage("Slika pozadine tiketa je obavezna.")
            .MaximumLength(1000).WithMessage("URL pozadinske slike ne moze imati vise od 1000 karaktera.");
    }
}
