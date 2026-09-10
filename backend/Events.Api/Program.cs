using DotNetEnv;
using Events.Api.Extensions;
using Events.Api.Middleware;

Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddSecurityServices();
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

app.SeedDatabase();

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