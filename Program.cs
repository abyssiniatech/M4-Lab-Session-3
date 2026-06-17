using Scalar.AspNetCore;
using TmsApi.Middleware;
using TmsApi.Models;
using TmsApi.Services;
using Microsoft.EntityFrameworkCore;
using TmsApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Problem details
builder.Services.AddProblemDetails();

// OpenAPI
builder.Services.AddOpenApi();

// 👇 ADD THIS: PostgreSQL DbContext registration
builder.Services.AddDbContext<TmsDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("TmsDatabase")
    )
);

// Your service layer
builder.Services.AddScoped<
    IEnrollmentService,
    EnrollmentService>();

var app = builder.Build();

// Development tools
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

// Middleware
app.UseMiddleware<CorrelationIdMiddleware>();

app.MapControllers();

// Test error endpoint
app.MapGet("/api/error", () =>
{
    throw new TmsDatabaseException("Simulated database failure");
});

app.Run();







































































































































// // method three 
// using Microsoft.EntityFrameworkCore;
// using TmsApi.Data;

// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<TmsDbContext>(options =>
//     options.UseNpgsql(
//         builder.Configuration.GetConnectionString("TmsDatabase")
//     )
// );

// var app = builder.Build();
// app.Run();