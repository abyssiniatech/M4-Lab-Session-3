

using Scalar.AspNetCore;
using TmsApi.Middleware;
using TmsApi.Models;
using TmsApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TmsApi.Data;
using TmsApi.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Problem details
builder.Services.AddProblemDetails();

// OpenAPI
builder.Services.AddOpenApi();

// PostgreSQL DbContext registration with logging
builder.Services.AddDbContext<TmsDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("TmsDatabase")
    )
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
);

// Service layer
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

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

// Custom middleware
app.UseMiddleware<CorrelationIdMiddleware>();

// Controllers
app.MapControllers();

// Test error endpoint
app.MapGet("/api/error", () =>
{
    throw new TmsDatabaseException("Simulated database failure");
});


using (var scope = app.Services.CreateScope())
{
    var Context = scope.ServiceProvider.GetRequiredService<TmsDbContext>();

    Context.Database.Migrate();
    if (!Context.Students.Any())
    {
        var Students = new List<Student>
        {
            new(){RegistrationNumber = "Tms-2026-001", Name = "Alice Johnson", Email = "alice.johnson@example.com", GPA = 3.8m,IsActive = true},
            new(){RegistrationNumber = "Tms-2026-002", Name = "Bob Smith", Email = "bob.smith@example.com", GPA = 3.6m},
            new(){RegistrationNumber = "Tms-2026-003", Name = "Charlie Brown", Email = "charlie.brown@example.com", GPA = 3.7m,IsActive = true},
            new(){RegistrationNumber = "Tms-2026-004", Name = "Diana Prince", Email = "diana.prince@example.com", GPA = 3.9m,IsActive = true},
            new(){RegistrationNumber = "Tms-2026-005", Name = "Ethan Hunt", Email = "ethan.hunt@example.com", GPA = 3.5m,IsActive = true},
            new(){RegistrationNumber = "Tms-2026-006", Name = "Fiona Gallagher", Email = "fiona.gallagher@example.com", GPA = 3.4m,IsActive = true},
            new(){RegistrationNumber = "Tms-2026-007", Name = "George Martin", Email = "george.martin@example.com", GPA = 3.3m,IsActive = true},
            new(){RegistrationNumber = "Tms-2026-008", Name = "Surafel Mengist", Email = "surafel.mengist@example.com", GPA = 3.2m,IsActive = true}
        };
        Context.Students.AddRange(Students);

        var Courses = new List<Course>
        {
            new(){Code = "MATH201", Title = "Calculus I", Capacity=30},
            new(){Code = "ENG301", Title = "English Literature", Capacity=25},
            new(){Code = "HIST101", Title = "World History",  Capacity=24},
            new(){Code = "BIO202", Title = "Biology II",  Capacity=40}
        };
        Context.Courses.AddRange(Courses);
        Context.SaveChanges();
        var Enrollments = new List<Enrollment>
        {
            new() { StudentId = Students[0].Id, CourseId =   Courses[0].Id, Grade = 4.0m },
            new() { StudentId = Students[0].Id, CourseId = Courses[1].Id, Grade = 3.8m },
            new() { StudentId = Students[1].Id, CourseId = Courses[0].Id, Grade = 3.6m },
            new() { StudentId = Students[1].Id, CourseId = Courses[2].Id, Grade = 3.5m },
            new() { StudentId = Students[2].Id, CourseId = Courses[1].Id, Grade = 3.7m },
            new() { StudentId = Students[2].Id, CourseId = Courses[3].Id, Grade = 3.9m },
        };
        Context.Enrollments.AddRange(Enrollments);
        Context.SaveChanges();





    }

}





app.Run();