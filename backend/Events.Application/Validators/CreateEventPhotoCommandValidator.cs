using Events.Application.Commands;
using FluentValidation;

namespace Events.Application.Validators;

public sealed class CreateEventPhotoCommandValidator : AbstractValidator<CreateEventPhotoCommand>
{
    public CreateEventPhotoCommandValidator()
    {
        RuleFor(x => x.dto.EventId)
            .NotEmpty().WithMessage("ID dogadjaja je obavezan.");
    }
}
