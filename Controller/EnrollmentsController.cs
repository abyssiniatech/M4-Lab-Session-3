using Microsoft.AspNetCore.Mvc;
using TmsApi.Models;
using TmsApi.Services;

namespace TmsApi.Controllers;

// add CourseRecord and StudentRecord to the Data class

[ApiController]
[Route("api/enrollments")]
public class EnrollmentsController
(
    IEnrollmentService enrollmentService
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult>
        GetAll()
    {
        var data =
            await enrollmentService.GetAllAsync();

        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult>
        GetById(string id)
    {
        var record =
            await enrollmentService
                .GetByIdAsync(id);

        return record is not null
            ? Ok(record)
            : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult>
        Create(
            [FromBody]
            CreateEnrollmentRequest request)
    {
        var record =
            await enrollmentService
                .EnrollAsync(
                    request.StudentId,
                    request.CourseCode);

        return CreatedAtAction(
            nameof(GetById),
            new { id = record.Id },
            record);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult>
        Delete(string id)
    {
        var deleted =
            await enrollmentService
                .DeleteAsync(id);

        return deleted
            ? NoContent()
            : NotFound();
    }
}