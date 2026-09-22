using Events.Application.Commands;
using FluentValidation;

namespace Events.Application.Validators;

public sealed class UpdateEventPhotoCommandValidator : AbstractValidator<UpdateEventPhotoCommand>
{
    public UpdateEventPhotoCommandValidator()
    {
        When(x => x.dto.Caption is not null, () =>
        {
            RuleFor(x => x.dto.Caption)
                .MaximumLength(500).WithMessage("Opis slike ne moze imati vise od 500 karaktera.");
        });
    }
}
