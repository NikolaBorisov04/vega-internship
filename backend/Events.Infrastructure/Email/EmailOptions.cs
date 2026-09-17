namespace Events.Infrastructure.Email;

public sealed class EmailOptions
{
    public const string SectionName = "Email";

    public string Email { get; set; } = "nikola.borisov2004@gmail.com";
    public string DisplayName { get; set; } = "Events";
    public string AppPassword { get; set; } = string.Empty;

    public string SmtpHost { get; set; } = "smtp.gmail.com";
    public int SmtpPort { get; set; } = 587;
}