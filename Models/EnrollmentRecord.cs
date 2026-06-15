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
)
{
    private string Id;
    private string name;
    private DateTime utcNow;
    private string v;

    // public CourseRecord(string v, string name, string description, DateTime utcNow)
    // {
    //     this.v = v;
    //     this.name = name;
    //     this.description = description;
    //     this.utcNow = utcNow;
    // }

     public CourseRecord(string id, string name, string description, DateTime utcNow)
     {
         this.Id   = id;
         this.name = name;
         this.description = description;
         this.utcNow = utcNow;
     }
}