using System.IdentityModel.Tokens.Jwt;
using EduConnect.Api.Abstractions.ServicesAbstractions;
using EduConnect.Api.Dtos.ClassDtos;
using EduConnect.Api.Exceptions;
using EduConnect.Api.Mappers.ClassMappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Api.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClassesController(
    IClassesService classesService,
    ILogger<ClassesController> logger) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Class>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetAllClassesAsync(CancellationToken abortionToken = default)
    {
        var academyId = GetAcademyIdFromToken();

        if (academyId == null)
            return Forbid("You do not have permission to access this resource.");

        var classes = await classesService.GetClassesByAcademyAsync(academyId.Value, abortionToken);
        return Ok(classes.Select(c => c.ToDto()));
    }

    [HttpGet("{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Class))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetClassByIdAsync([FromRoute] Guid id, CancellationToken abortionToken = default)
    {
        try
        {
            var classEntity = await classesService.GetClassByIdAsync(id, abortionToken);
            return Ok(classEntity.ToDto());
        }
        catch (ClassNotFoundException)
        {
            return NotFound($"Class with ID {id} not found.");
        }
    }

    // Modification is needed
    // [Authorize(Roles = "Teacher")]
    [HttpGet("my-classes")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Class>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetTeachersClassesAsync(CancellationToken abortionToken = default)
    {
        var teacherId = GetUserIdFromToken()!;
        var academyId = GetAcademyIdFromToken()!;
        // Console.WriteLine(teacherId.Value);

        if (teacherId == null)
            return Forbid("You do not have permission to access this resource.");

        var classes = await classesService.GetClassesByTeacherAsync(academyId.Value, teacherId.Value, abortionToken);
        return Ok(classes.Where(c => c.TeacherId == teacherId.Value).Select(c => c.ToDto()));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateClassAsync([FromBody] CreateClass classDto, CancellationToken cancellationToken = default)
    {
        var academyId = GetAcademyIdFromToken();

        if (academyId == null)
            return Forbid("You do not have permission to create a class.");

        var newClass = classDto.ToEntity();
        newClass.AcademyId = academyId;

        try
        {
            var createdClass = await classesService.CreateClassAsync(newClass, cancellationToken);
            return Ok("Successfully created");//CreatedAtAction(nameof(GetClassByIdAsync), new { id = createdClass.Id }, createdClass.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating class.");
            return StatusCode(500, "An error occurred while creating the class.");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteClassAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            await classesService.DeleteClassAsync(id, cancellationToken);
            return NoContent();
        }
        catch (ClassNotFoundException)
        {
            return NotFound($"Class with ID {id} not found.");
        }
    }

    private Guid? GetAcademyIdFromToken()
    {
        var academyIdClaim = User.Claims.FirstOrDefault(c => c.Type == "AcademyId")?.Value;
        return Guid.TryParse(academyIdClaim, out var academyId) ? academyId : null;
    }

    private Guid? GetUserIdFromToken()
    {
        var idClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        return Guid.TryParse(idClaim, out var id) ? id : null;
    }
}