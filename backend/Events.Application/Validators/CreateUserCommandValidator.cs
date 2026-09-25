using Events.Application.Commands;
using FluentValidation;
using PhoneNumbers;

namespace Events.Application.Validators;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly PhoneNumberUtil _phoneNumberUtil;
    public CreateUserCommandValidator()
    {
        _phoneNumberUtil = PhoneNumberUtil.GetInstance();

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("User name is required.")
            .MaximumLength(20).WithMessage("User name must be less than 20 characters.")
            .MinimumLength(3).WithMessage("User name must be at least 3 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("The entered email address is not valid.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        RuleFor(x => x.PhoneNumber)
            .Must(BeValidPhoneNumber)
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithMessage("The entered phone number is not valid.");
    }
    private bool BeValidPhoneNumber(string? phoneNumber)
    {
        try
        {
            var parsedNumber = _phoneNumberUtil.Parse(phoneNumber, null);

            return _phoneNumberUtil.IsValidNumber(parsedNumber);
        }
        catch (NumberParseException)
        {
            return false;
        }
    }
}