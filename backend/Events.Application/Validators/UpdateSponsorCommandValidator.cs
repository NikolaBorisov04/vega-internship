using Events.Application.Commands;
using FluentValidation;

namespace Events.Application.Validators;

public sealed class UpdateSponsorCommandValidator : AbstractValidator<UpdateSponsorCommand>
{
    public UpdateSponsorCommandValidator()
    {
        When(x => x.Dto.Name is not null, () =>
        {
            RuleFor(x => x.Dto.Name)
                .NotEmpty().WithMessage("Ime ne moze biti prazno.")
                .MaximumLength(20).WithMessage("Ime ne moze imati vise od 20 karaktera.");
        });

        When(x => x.Dto.ContactEmail is not null, () =>
        {
            RuleFor(x => x.Dto.ContactEmail)
                .NotEmpty().WithMessage("Email ne moze biti prazan.")
                .EmailAddress().WithMessage("Email format nije validan.")
                .MaximumLength(150).WithMessage("Email ne moze imati vise od 150 karaktera.");
        });

        When(x => x.Dto.Description is not null, () =>
        {
            RuleFor(x => x.Dto.Description)
                .NotEmpty().WithMessage("Opis ne moze biti prazan.")
                .MaximumLength(500).WithMessage("Opis ne moze imati vise od 500 karaktera.");
        });

        When(x => x.Dto.LogoUrl is not null, () =>
        {
            RuleFor(x => x.Dto.LogoUrl)
                .NotEmpty().WithMessage("URL logotipa ne moze biti prazan.")
                .MaximumLength(1000).WithMessage("URL logotipa ne moze imati vise od 1000 karaktera.");
        });

        When(x => x.Dto.TaxId is not null, () =>
        {
            RuleFor(x => x.Dto.TaxId)
                .NotEmpty().WithMessage("PIB ne moze biti prazan.")
                .MaximumLength(50).WithMessage("PIB ne moze imati vise od 50 karaktera.");
        });

        When(x => x.Dto.WebsiteUrl is not null, () =>
        {
            RuleFor(x => x.Dto.WebsiteUrl)
                .MaximumLength(1000).WithMessage("URL veb sajta ne moze imati vise od 1000 karaktera.");
        });
    }
}
