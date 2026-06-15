// CourseService implementation
using TmsApi.Data;
using TmsApi.Models;
using TmsApi.Services;


public class CourseService : ICourseService
{
    private readonly ILogger<CourseService> _logger;

    public CourseService(ILogger<CourseService> logger)
    {
        _logger = logger;
    }

    public async Task<List<CourseRecord>> GetAllAsync()
    {
        await Task.Delay(100);

        return FakeDatabase.Courses;
    }

    public async Task<CourseRecord?> GetByIdAsync(string id)
    {
        await Task.Delay(100);

        return FakeDatabase.Courses.FirstOrDefault(x => x.Id == id);
    }

    public async Task<CourseRecord> CreateAsync(string name, string description)
    {
        var record = new CourseRecord(
            Guid.NewGuid().ToString(),
            name,
            description,
            DateTime.UtcNow);

        FakeDatabase.Courses.Add(record);

        _logger.LogInformation("Course created {Id}", record.Id);

        await Task.Delay(100);

        return record;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var course = FakeDatabase.Courses.FirstOrDefault(x => x.Id == id);
        if (course == null)
        {
            return false;
        }

        FakeDatabase.Courses.Remove(course);

        _logger.LogInformation("Course deleted {Id}", id);

        await Task.Delay(100);

        return true;
    }
}