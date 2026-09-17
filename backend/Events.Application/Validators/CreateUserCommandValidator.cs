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
            .NotEmpty().WithMessage("Ime korisnika je obavezno.")
            .MaximumLength(20).WithMessage("Ime korisnika mora imati manje od 20 karaktera.")
            .MinimumLength(3).WithMessage("Ime korisnika mora imati barem 3 karaktera.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email je obavezan.")
            .EmailAddress().WithMessage("Uneta email adresa nije validna.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Sifra je obavezna.")
            .MinimumLength(6).WithMessage("Sifra korisnika mora imati barem 6 karaktera.");
        RuleFor(x => x.PhoneNumber)
            .Must(BeValidPhoneNumber)
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithMessage("Uneti broj telefona nije validan.");
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