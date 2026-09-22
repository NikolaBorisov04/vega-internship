using Events.Application.Commands;
using FluentValidation;

namespace Events.Application.Validators;

public sealed class UpdateTicketCommandValidator : AbstractValidator<UpdateTicketCommand>
{
    public UpdateTicketCommandValidator()
    {
        When(x => x.dto.QRCodeURL is not null, () =>
        {
            RuleFor(x => x.dto.QRCodeURL)
                .NotEmpty().WithMessage("QR kod URL ne moze biti prazan.")
                .MaximumLength(1000).WithMessage("QR kod URL ne moze imati vise od 1000 karaktera.");
        });

        When(x => x.dto.SeatNumber.HasValue, () =>
        {
            RuleFor(x => x.dto.SeatNumber!.Value)
                .GreaterThan(0).WithMessage("Broj sedista mora biti veci od 0.");
        });
    }
}
