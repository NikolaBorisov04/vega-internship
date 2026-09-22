using Events.Application.Commands;
using FluentValidation;

namespace Events.Application.Validators;

public sealed class CreateEventCommandValidator
    : AbstractValidator<CreateEventCommand>
{
    private const long MaxFileSize = 5 * 1024 * 1024;

    private static readonly string[] AllowedContentTypes =
    [
        "image/jpeg",
        "image/png",
        "image/webp"
    ];

    public CreateEventCommandValidator()
    {
        RuleFor(x => x.dto.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.dto.Description)
            .NotEmpty();

        RuleFor(x => x.dto.Country)
            .NotEmpty();

        RuleFor(x => x.dto.City)
            .NotEmpty();

        RuleFor(x => x.dto.Address)
            .NotEmpty();

        RuleFor(x => x.dto.EndOfEvent)
            .GreaterThan(x => x.dto.StartOfEvent)
            .WithMessage("End of event must be after start of event.");

        RuleFor(x => x.MainImage)
            .NotNull()
            .WithMessage("Main event image is required.");

        RuleFor(x => x.MainImage.Length)
            .LessThanOrEqualTo(MaxFileSize)
            .WithMessage("Main event image cannot exceed 5 MB.");

        RuleFor(x => x.MainImage.ContentType)
            .Must(contentType =>
                AllowedContentTypes.Contains(contentType))
            .WithMessage("Only JPEG, PNG and WebP images are allowed.");
    }
}