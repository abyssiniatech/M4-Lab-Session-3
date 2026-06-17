namespace TmsApi.Models;

public record EnrollmentRecord
(
    string Id,
    string StudentId,
    string CourseCode,
    DateTime CreatedAt

);


// student record
public record StudentRecord
(
    string Id,
    string Name,
    string Email,
    DateTime CreatedAt
);


// course record
public record CourseRecord(
    string Id,
    string CourseCode,
    string CourseName,
    string description,
    DateTime CreatedAt
);