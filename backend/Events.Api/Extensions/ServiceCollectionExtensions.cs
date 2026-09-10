using Microsoft.OpenApi;
using Microsoft.EntityFrameworkCore;
using Events.Api.Data;
using Events.Api.Services;
using Events.Api.Mappings;
using Events.Api.Middleware;

namespace Events.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("TestDatabase"));

        services.AddSingleton<ResponseMapper>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<ISponsorService, SponsorService>();

        services.AddControllers(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
            // Ovo je po default true, sklonio sam ga zato sto mi je brisalo "Async" ime rute i onda kad pozovem u kontroler za create GetByIdAsync prijavljuje error 500 jer se poziva na rutu GetById
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                // MORA SA MALO SLOVO B DA SE UKUCA bearer
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Unesite vaš JWT token."
            });

            options.AddSecurityRequirement(x => new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecuritySchemeReference("Bearer", x),
                    new List<string>()
                }
            });
        });

        services.AddExceptionHandler<ExceptionMiddleware>();
        services.AddProblemDetails();

        return services;
    }

    public static WebApplication SeedDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        TestDataSeeder.Seed(context);

        return app;
    }
}