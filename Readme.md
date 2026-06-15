<!-- What Are We Building?

We are building an API that manages enrollments.

Think of a university system.

A student wants to enroll in a course.

Example:

Student: S-001

Course: CS-101

The system stores:

{
  "id": "1",
  "studentId": "S-001",
  "courseCode": "CS-101"
}
Step 1: Create Project

Create API project:

dotnet new webapi -n TmsApi

Enter project:

cd TmsApi

Open VS Code:

code .
Step 2: Understand Project Structure
TmsApi
│
├── Controllers
├── Services
├── Models
├── Data
├── Middleware
│
├── appsettings.json
├── Program.cs
│
└── TmsApi.csproj

Think of each folder as a team member.

Step 3: Models Folder
Purpose

Models describe data.

Question:

What does an Enrollment look like?

Answer:

public record EnrollmentRecord
(
    string Id,
    string StudentId,
    string CourseCode,
    DateTime CreatedAt
);

This is called a Model.

Example
{
    "id":"1",
    "studentId":"S-001",
    "courseCode":"CS-101",
    "createdAt":"2026-06-15"
}

The model defines this shape.

Step 4: Data Folder

Create:

Data
└── FakeDatabase.cs

Purpose:

Store data temporarily.

public static List<EnrollmentRecord>
Enrollments = [];

Think:

Notebook

instead of

SQL Server

For learning CRUD this is enough.

Step 5: Service Layer

Create:

Services
│
├── IEnrollmentService.cs
└── EnrollmentService.cs
Why Service Layer?

Bad:

Controller
   |
Database

Controller becomes messy.

Good:

Controller
   |
Service
   |
Database

Service contains business logic.

Interface
public interface IEnrollmentService
{
    Task<List<EnrollmentRecord>> GetAllAsync();

    Task<EnrollmentRecord?> GetByIdAsync(string id);

    Task<EnrollmentRecord> EnrollAsync(
        string studentId,
        string courseCode);

    Task<bool> DeleteAsync(string id);
}

Think:

Contract

The service MUST implement these methods.

Service Implementation
public class EnrollmentService
    : IEnrollmentService

This contains the real code.

Example:

public async Task<List<EnrollmentRecord>>
GetAllAsync()
{
    return FakeDatabase.Enrollments;
}

Meaning:

Get all enrollments
Step 6: Controller

Create:

Controllers
└── EnrollmentsController.cs

Purpose:

Receive HTTP requests.

Example:

GET /api/enrollments

Controller receives this request.

Controller:

[ApiController]
[Route("api/enrollments")]

Creates route:

/api/enrollments
Dependency Injection

Constructor:

public EnrollmentsController(
    IEnrollmentService service)
{
}

Question:

Who creates EnrollmentService?

Answer:

Program.cs

Step 7: Program.cs

Most important file.

Think:

Application Startup

Register service:

builder.Services.AddScoped<
    IEnrollmentService,
    EnrollmentService>();

Meaning:

Whenever somebody asks for
IEnrollmentService

Give them
EnrollmentService

Enable controllers:

builder.Services.AddControllers();

Map controllers:

app.MapControllers();

Without this:

404 Not Found

for every endpoint.

Step 8: CRUD Operations

CRUD means:

C = Create
R = Read
U = Update
D = Delete
CREATE

HTTP:

POST

Controller:

[HttpPost]

Request:

{
  "studentId":"S-001",
  "courseCode":"CS-101"
}

Flow:

POST
  │
  ▼
Controller
  │
  ▼
Service
  │
  ▼
FakeDatabase

New enrollment saved.

Response:

201 Created
READ ALL

HTTP:

GET

Controller:

[HttpGet]

Request:

GET /api/enrollments

Response:

[
   {
      "id":"1",
      "studentId":"S-001"
   }
]
READ BY ID

Request:

GET /api/enrollments/1

Controller:

[HttpGet("{id}")]

Returns one record.

UPDATE

HTTP:

PUT

Request:

PUT /api/enrollments/1

Body:

{
   "studentId":"S-002",
   "courseCode":"CS-201"
}

Service updates data.

Response:

200 OK
DELETE

HTTP:

DELETE /api/enrollments/1

Controller:

[HttpDelete("{id}")]

Response:

204 No Content

Record removed.

Step 9: Middleware

Create:

Middleware
└── CorrelationIdMiddleware.cs

Purpose:

Runs before controllers.

Flow:

Request
   │
Middleware
   │
Controller

Adds:

X-Correlation-Id

header.

Useful for logging.

Step 10: ProblemDetails

Register:

builder.Services.AddProblemDetails();

Enable:

app.UseExceptionHandler();

Without ProblemDetails:

HTML Error Page

With ProblemDetails:

{
  "type":"...",
  "title":"Server Error",
  "status":500,
  "detail":"..."
}

Professional API response.

Step 11: Scalar

Install:

dotnet add package Scalar.AspNetCore

Program.cs:

builder.Services.AddOpenApi();

app.MapOpenApi();

app.MapScalarApiReference();

Run:

dotnet run

Open:

http://localhost:5000/scalar/v1

You can test APIs without Postman or curl.

Step 12: Complete Request Lifecycle

User clicks:

POST /api/enrollments

JSON:

{
  "studentId":"S-001",
  "courseCode":"CS-101"
}

What happens?

1 Request arrives

       │

2 Middleware runs

       │

3 Controller receives request

       │

4 Controller calls Service

       │

5 Service validates

       │

6 Service stores data

       │

7 Controller returns response

       │

8 Browser receives

201 Created
Step 13: Testing CRUD

Start API:

dotnet run

Open:

http://localhost:5000/scalar/v1
Test Create
{
  "studentId":"S-001",
  "courseCode":"CS-101"
}

Expected:

201 Created
Test Read All
GET /api/enrollments

Expected:

[
  {
    "id":"...",
    "studentId":"S-001",
    "courseCode":"CS-101"
  }
]
Test Read By Id
GET /api/enrollments/{id}

Expected:

200 OK
Test Delete
DELETE /api/enrollments/{id}

Expected:

204 No Content
Test Error Endpoint
GET /api/error

Expected:

{
  "title":"An error occurred",
  "status":500
}
The Most Important Concepts in This Lab
Model → Defines data structure.
Controller → Receives HTTP requests.
Service → Contains business logic.
Dependency Injection → Creates and injects services automatically.
Middleware → Runs before/after requests.
Program.cs → Application startup and configuration.
CRUD → Create, Read, Update, Delete.
ProblemDetails → Standard error responses.
OpenAPI/Scalar → API documentation and testing.
HTTP Status Codes → 200, 201, 204, 404, 500. -->