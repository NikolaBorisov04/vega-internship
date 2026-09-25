using Events.Application.Commands;
using FluentValidation;

namespace Events.Application.Validators;

public sealed class CreateTicketTypeCommandValidator : AbstractValidator<CreateTicketTypeCommand>
{
    public CreateTicketTypeCommandValidator()
    {
        RuleFor(x => x.Dto.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Dto.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");

        RuleFor(x => x.Dto.EventId)
            .NotEmpty().WithMessage("EventId is required.");

        RuleFor(x => x.Dto.Description)
            .NotEmpty().WithMessage("Ticket type description is required.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(x => x.Dto.QuantityAvailable)
            .GreaterThanOrEqualTo(0).WithMessage("Available quantity must be greater than or equal to 0.");
    }
}
