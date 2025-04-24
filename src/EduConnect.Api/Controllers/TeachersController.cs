using EduConnect.Api.Abstractions.ServicesAbstractions;
using EduConnect.Api.Dtos.TeacherDtos;
using EduConnect.Api.Exceptions;
using EduConnect.Api.Mappers.TeacherMappers;
using EduConnect.Api.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeachersController(
    ITeachersService teachersService
    /*ILogger<TeachersController> logger*/) : ControllerBase
{
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Teacher>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllTeachersAsync(CancellationToken abortionToken = default)
    {
        var academyId = JwtClaimHelper.GetAcademyIdFromToken(User);
        if (academyId == null)
            return Forbid("You do not have permission to access this resource.");

        var teachers = await teachersService.GetAllTeachersAsync(academyId.Value, abortionToken);
        return Ok(teachers.Select(t => t.ToDto()));
    }

    [HttpGet("{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Teacher))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTeacherByIdAsync(
        [FromRoute] Guid id,
        CancellationToken abortionToken = default)
    {
        try
        {
            var teacher = await teachersService.GetTeacherByIdAsync(id, abortionToken);
            return Ok(teacher.ToDto());
        }
        catch (TeacherNotFoundException)
        {
            return NotFound($"Teacher with ID {id} not found.");
        }
    }

    [HttpDelete("{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTeacherAsync([FromRoute] Guid id, CancellationToken abortionToken = default)
    {
        try
        {
            await teachersService.DeleteTeacherAsync(id, abortionToken);
            return NoContent();
        }
        catch(TeacherNotFoundException)
        {
            return NotFound($"Teacher with ID {id} not found.");
        }
    }
}