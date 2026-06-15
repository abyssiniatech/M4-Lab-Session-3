// 2. StudentService implementation
using TmsApi.Data;
using TmsApi.Models;
using TmsApi.Services;

public class StudentService : IStudentService
{
    private readonly ILogger<StudentService> _logger;

    public StudentService(ILogger<StudentService> logger)
    {
        _logger = logger;
    }

    public async Task<List<StudentRecord>> GetAllAsync()
    {
        await Task.Delay(100);

        return FakeDatabase.Students;
    }

    public async Task<StudentRecord?> GetByIdAsync(string id)
    {
        await Task.Delay(100);

        return FakeDatabase.Students.FirstOrDefault(x => x.Id == id);
    }

    public async Task<StudentRecord> CreateAsync(string name, string email)
    {
        var record = new StudentRecord(
            Guid.NewGuid().ToString(),
            name,
            email,
            DateTime.UtcNow);

        FakeDatabase.Students.Add(record);

        _logger.LogInformation("Student created {Id}", record.Id);

        await Task.Delay(100);

        return record;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var student = FakeDatabase.Students.FirstOrDefault(x => x.Id == id);
        if (student == null)
        {
            return false;
        }

        FakeDatabase.Students.Remove(student);

        _logger.LogInformation("Student deleted {Id}", id);

        await Task.Delay(100);

        return true;
    }
}