using TmsApi.Data;
using TmsApi.Models;

namespace TmsApi.Services;

public class EnrollmentService
    : IEnrollmentService
{
    private readonly ILogger<EnrollmentService> _logger;

    public EnrollmentService(
        ILogger<EnrollmentService> logger)
    {
        _logger = logger;
    }

    public async Task<List<EnrollmentRecord>>
        GetAllAsync()
    {
        _logger.LogInformation(
            "Getting enrollments");

        await Task.Delay(100);

        return FakeDatabase.Enrollments;
    }

    public async Task<EnrollmentRecord?>
        GetByIdAsync(string id)
    {
        await Task.Delay(100);

        return FakeDatabase
            .Enrollments
            .FirstOrDefault(x => x.Id == id);
    }

    public async Task<EnrollmentRecord>
        EnrollAsync(
            string studentId,
            string courseCode)
    {
        await Task.Delay(100);

        var record =
            new EnrollmentRecord(
                Guid.NewGuid().ToString(),
                studentId,
                courseCode,
                DateTime.UtcNow);

        FakeDatabase.Enrollments.Add(record);

        _logger.LogInformation(
            "Enrollment created {Id}",
            record.Id);

        return record;
    }

    public async Task<bool>
        DeleteAsync(string id)
    {
        await Task.Delay(100);

        var record =
            FakeDatabase.Enrollments
                .FirstOrDefault(x => x.Id == id);

        if (record is null)
        {
            _logger.LogWarning(
                "Enrollment not found");

            return false;
        }

        FakeDatabase.Enrollments.Remove(record);

        _logger.LogInformation(
            "Enrollment deleted");

        return true;
    }
}