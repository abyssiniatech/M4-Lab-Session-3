

using Scalar.AspNetCore;
using TmsApi.Middleware;
using TmsApi.Models;
using TmsApi.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();


builder.Services.AddScoped<
    IEnrollmentService,
    EnrollmentService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}
else
{
    app.UseExceptionHandler();
}

app.UseStatusCodePages();

app.UseMiddleware<
    CorrelationIdMiddleware>();

app.MapControllers();

app.MapGet("/api/error", () =>
{
    throw new TmsDatabaseException(
        "Simulated database failure");
});

app.Run();