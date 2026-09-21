using DotNetEnv;
using Events.Api.Extensions;
using Events.Api.Middleware;
using Events.Infrastructure.Extensions;

Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddJwtAuthentication();
builder.Services.AddSecurityServices();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseCors("Events");
app.UseExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
app.UseMiddleware<LoggingMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();