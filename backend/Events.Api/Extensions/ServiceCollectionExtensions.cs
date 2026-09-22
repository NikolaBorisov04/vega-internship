using Microsoft.OpenApi;
using Microsoft.EntityFrameworkCore;
using Events.Infrastructure.Persistence.Data;
using Events.Application.Services;
using Events.Application.Mappers;
using Events.Api.Middleware;
using Events.Infrastructure.Persistence.Repositories;
using Events.Application.Repositories;
using Events.Application;
using FluentValidation;
using MediatR;
using Events.Application.Behaviors;
using System.Text.Json.Serialization;

namespace Events.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is not defined.");
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        services.AddCors(options =>
        {
            options.AddPolicy("Events", policy =>
            {
                policy
                    .WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        

        services.AddSingleton<ResponseMapper>();
        services.AddSingleton<CommandMapper>();

        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IUnitOfWork>(
            sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ISponsorRepository, SponsorRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
        services.AddScoped<IEventSponsorshipRepository, EventSponsorshipRepository>();
        services.AddScoped<IEventPhotoRepository, EventPhotoRepository>();
        services.AddScoped<IOrganizerRepository, OrganizerRepository>();

        services.AddApplication();

        services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddControllers(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
            // Ovo je po default true, sklonio sam ga zato sto mi je brisalo "Async" ime rute i onda kad pozovem u kontroler za create GetByIdAsync prijavljuje error 500 jer se poziva na rutu GetById
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(
                new JsonStringEnumConverter()
            );
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
                Description = "Unesite vas JWT token."
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
}