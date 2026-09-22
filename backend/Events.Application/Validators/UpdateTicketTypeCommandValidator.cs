using Events.Application.Commands;
using FluentValidation;

namespace Events.Application.Validators;

public sealed class UpdateTicketTypeCommandValidator : AbstractValidator<UpdateTicketTypeCommand>
{
    public UpdateTicketTypeCommandValidator()
    {
        When(x => x.dto.Name is not null, () =>
        {
            RuleFor(x => x.dto.Name)
                .NotEmpty().WithMessage("Ime ne moze biti prazno.")
                .MaximumLength(100).WithMessage("Ime ne moze imati vise od 100 karaktera.");
        });

        When(x => x.dto.Price.HasValue, () =>
        {
            RuleFor(x => x.dto.Price!.Value)
                .GreaterThanOrEqualTo(0).WithMessage("Cena mora biti veca ili jednaka 0.");
        });

        When(x => x.dto.Description is not null, () =>
        {
            RuleFor(x => x.dto.Description)
                .MaximumLength(500).WithMessage("Opis ne moze imati vise od 500 karaktera.");
        });

        When(x => x.dto.QuantityAvailable.HasValue, () =>
        {
            RuleFor(x => x.dto.QuantityAvailable!.Value)
                .GreaterThanOrEqualTo(0).WithMessage("Dostupna kolicina mora biti veca ili jednaka 0.");
        });

        When(x => x.dto.TicketBackgroundImageUrl is not null, () =>
        {
            RuleFor(x => x.dto.TicketBackgroundImageUrl)
                .MaximumLength(1000).WithMessage("URL pozadinske slike ne moze imati vise od 1000 karaktera.");
        });
    }
}
