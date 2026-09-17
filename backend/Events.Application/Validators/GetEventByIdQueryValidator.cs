using Events.Application.Queries;
using FluentValidation;

namespace Events.Application.Validators;

public class GetEventByIdQueryValidator : AbstractValidator<GetEventByIdQuery>
{
    public GetEventByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage("Id mora da bude tipa GUID.");
    }
}