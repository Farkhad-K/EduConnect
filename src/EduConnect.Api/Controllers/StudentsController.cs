using EduConnect.Api.Abstractions.ServicesAbstractions;
using EduConnect.Api.Dtos.StudentDtos;
using EduConnect.Api.Exceptions;
using EduConnect.Api.Mappers.StudentMappers;
using EduConnect.Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StudentsController(
    IStudentsService studentsService,
    IClassesService classesService,
    ILogger<StudentsController> logger) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Student>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetAllStudentsAsync(CancellationToken abortionToken = default)
    {
        var academyId = JwtClaimHelper.GetAcademyIdFromToken(User);

        if (academyId == null)
            return Forbid("You do not have permission to access this resource.");

        var students = await studentsService.GetAllStudentsByAcademyIdAsync(academyId.Value, abortionToken);
        return Ok(students.Select(s => s.ToDto()));
    }

    [HttpGet("{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStudentByIdAsync(Guid id, CancellationToken abortionToken = default)
    {
        try
        {
            var student = await studentsService.GetStudentByIdAsync(id, abortionToken);
            return Ok(student.ToDto());
        }
        catch (StudentNotFoundException)
        {
            return NotFound($"Student with ID {id} not found.");
        }
    }

    // Change the method to handle creating a student with classes in service layer
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateStudentAsync([FromBody] CreateStudent studentDto, CancellationToken abortionToken = default)
    {
        var academyId = JwtClaimHelper.GetAcademyIdFromToken(User);

        if (academyId == null)
            return Forbid("You do not have permission to create a student.");

        var student = studentDto.ToEntity(academyId.Value);

        try
        {
            if (studentDto.ClassIds.Any())
            {
                foreach (var classId in studentDto.ClassIds)
                {
                    var classEntity = await classesService.GetClassByIdAsync(classId, abortionToken);
                    classEntity.Students.Add(student);
                }
            }

            var createdStudent = await studentsService.CreateStudentAsync(student, abortionToken);

            return Ok(createdStudent.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating student.");
            return StatusCode(500, "An error occurred while creating the student.");
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces("application/json")]
    [HttpGet("by-token/{token}")]
    public async ValueTask<IActionResult> GetStudentByTokenAsync([FromRoute] string token, CancellationToken abortionToken = default)
    {
        try
        {
            var student = await studentsService.GetStudentByTokenAsync(token, abortionToken);
            return Ok(student.ToDto());
        }
        catch (StudentWithTokenNotFoundException)
        {
            return NotFound($"Student with token {token} not found.");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> DeleteStudent([FromRoute] Guid id, CancellationToken abortionToken = default)
    {
        try
        {
            await studentsService.DeleteStudentAsync(id, abortionToken);
            return NoContent();
        }
        catch (StudentNotFoundException)
        {
            return NotFound($"Student with ID {id} not found.");
        }
    }

    // private Guid? GetAcademyIdFromToken()
    // {
    //     var academyIdClaim = User.Claims.FirstOrDefault(c => c.Type == "AcademyId")?.Value;
    //     return Guid.TryParse(academyIdClaim, out var academyId) ? academyId : null;
    // }
}