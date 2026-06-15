using TmsApi.Models;

namespace TmsApi.Services;



// 1.EnrollmentService interface
public interface IEnrollmentService
{
    Task<List<EnrollmentRecord>> GetAllAsync();

    Task<EnrollmentRecord?> GetByIdAsync(string id);

    Task<EnrollmentRecord> EnrollAsync(
        string studentId,
        string courseCode);

    Task<bool> DeleteAsync(string id);
}




//2. StudentRecord interface 
public interface IStudentService
{
    Task<List<StudentRecord>> GetAllAsync();

    Task<StudentRecord?> GetByIdAsync(string id);

    Task<StudentRecord> CreateAsync(
        string name,
        string email);

    Task<bool> DeleteAsync(string id);
}


//3. CourseRecord interface
public interface ICourseService
{
    Task<List<CourseRecord>> GetAllAsync();

    Task<CourseRecord?> GetByIdAsync(string courseCode);

    Task<CourseRecord> CreateAsync(
        string courseCode,
        string courseName);

    Task<bool> DeleteAsync(string courseCode);
}