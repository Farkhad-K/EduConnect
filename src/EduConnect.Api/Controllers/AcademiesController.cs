using EduConnect.Api.Abstractions.ServicesAbstractions;
using EduConnect.Api.Dtos.AcademyDtos;
using EduConnect.Api.Exceptions;
using EduConnect.Api.Mappers.AcademyMappers;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Api.Controllers;

// [Authorize(Roles = "SuperAdmin")]
[ApiController]
[Route("api/[controller]")]
public class AcademiesController(
    IAcademiesService academiesService,
    ILogger<AcademiesController> logger) : ControllerBase
{
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Academy>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAcademies(CancellationToken cancellationToken = default)
    {
        var academies = await academiesService.GetAllAcademiesAsync(cancellationToken);
        return Ok(academies.Select(a => a.ToDto()));
    }

    [HttpGet("{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Academy))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAcademyByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var academy = await academiesService.GetAcademyByIdAsync(id, cancellationToken);
            return Ok(academy.ToDto());
        }
        catch (AcademyNotFoundException)
        {
            return NotFound($"Academy with ID {id} not found.");
        }
    }


    [HttpGet("by-token/{token}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Academy))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAcademyByTokenAsync([FromRoute] string token, CancellationToken cancellationToken = default)
    {
        try
        {
            var academy = await academiesService.GetAcademyByTokenAsync(token, cancellationToken);
            return Ok(academy.ToDto());
        }
        catch (AcademyWithTokenNotFoundException)
        {
            return NotFound($"Academy with token '{token}' not found.");
        }
    }

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAcademyAsync([FromBody] CreateAcademy academyDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var createdAcademy = await academiesService.CreateAcademyAsync(academyDto.ToEntity(), cancellationToken);
            // return CreatedAtAction(nameof(GetAcademyByIdAsync), new { id = createdAcademy.Id }, createdAcademy.ToDto());
            return Ok("Created");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating academy");
            return StatusCode(500, "An error occurred while creating the academy.");
        }
    }

    [HttpPut("{id:guid}")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Academy))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAcademyAsync([FromRoute] Guid id, [FromBody] UpdateAcademy academyDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var updatedAcademy = await academiesService.UpdateAcademyAsync(id, academyDto.UpdatedToEntity(), cancellationToken);
            return Ok(updatedAcademy.ToDto());
        }
        catch (AcademyNotFoundException)
        {
            return NotFound($"Academy with ID {id} not found.");
        }
    }

    [HttpDelete("{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAcademyAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            await academiesService.DeleteAcademyAsync(id, cancellationToken);
            return NoContent(); // More RESTful than returning a string
        }
        catch (AcademyNotFoundException)
        {
            return NotFound($"Academy with ID {id} not found.");
        }
    }
}
