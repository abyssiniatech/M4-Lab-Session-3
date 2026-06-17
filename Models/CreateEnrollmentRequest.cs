namespace TmsApi.Models;

public record CreateEnrollmentRequest
(
    string StudentId,
    string CourseCode
);


public record CreateStudentRequest
(
    string Name,
    string Email
);

public record CreateCourseRequest
(
    string CourseCode,
    string CourseName,
    string Description
);