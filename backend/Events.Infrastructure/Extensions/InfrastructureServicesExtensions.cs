using CloudinaryDotNet;
using Events.Application.Services;
using Events.Application.Storage;
using Events.Infrastructure.Email;
using Events.Infrastructure.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Events.Infrastructure.Extensions;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EmailOptions>(
            configuration.GetSection(EmailOptions.SectionName));

        services.AddScoped<IEmailService, GmailEmailService>();

        var cloudinaryUrl = Environment.GetEnvironmentVariable("CLOUDINARY_URL");

        if (string.IsNullOrWhiteSpace(cloudinaryUrl))
        {
            throw new InvalidOperationException(
                "CLOUDINARY_URL environment variable is not configured.");
        }

        var cloudinary = new Cloudinary(cloudinaryUrl);

        services.AddSingleton(cloudinary);

        services.AddScoped<IImageStorage, CloudinaryImageStorage>();

        return services;
    }
}