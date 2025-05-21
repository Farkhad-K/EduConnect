using System.Net.Http.Headers;
using EduConnect.Api.Abstractions.ServicesAbstractions;
using EduConnect.Api.Dtos.ParentDtos;
using EduConnect.Api.Mappers.ParentMappers;
using EduConnect.Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParentsController(
    IParentsService parentsService) : ControllerBase
{
    // [HttpGet]
    // [Produces("application/json")]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Parent>))]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async ValueTask<IActionResult> GetAllMyChildren(CancellationToken abortionToken = default)
    // {
    //     var parentId = JwtClaimHelper.GetUserIdFromToken(User);
    //     if (parentId is null) return Forbid("You do not have permission to access this resource.");

    //     var children = await parentsService.GetChildrenOfParent(parentId.Value, abortionToken);

    //     return Ok(children.ToDto());
    // }

    [Authorize(Roles = "Parent")]
    [HttpGet("me")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Parent))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCurrentParent(CancellationToken token = default)
    {
        var parentId = JwtClaimHelper.GetUserIdFromToken(User);
        if (parentId is null)
            return Unauthorized();

        var parent = await parentsService.GetChildrenOfParent(parentId.Value, token);
        return Ok(parent.ToDto());
    }


    [Authorize(Roles = "Parent")]
    [HttpPost("attach-student")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Parent))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> AttachStudentToParent([FromBody] AttachStudentsToParentDto child, CancellationToken abortionToken = default)
    {
        var parentId = JwtClaimHelper.GetUserIdFromToken(User);
        if (parentId is null) return Forbid("You do not have permission to access this resource.");

        var result = await parentsService.AttachStudentToParent(parentId.Value, child.StudentId, abortionToken);
        if (result)
            return Ok("Child attachet to parent successfully.");

        return BadRequest("Failed to attach child to parent.");
    }

    [Authorize(Roles = "Parent")]
    [HttpDelete("detach-student/{studentId:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DetachStudentFromParent([FromRoute] Guid studentId, CancellationToken abortionToken = default)
    {
        var parentId = JwtClaimHelper.GetUserIdFromToken(User);
        if (parentId is null) return Forbid("You do not have permission to access this resource.");

        var result = await parentsService.RemoveStudentFromParent(parentId.Value, studentId, abortionToken);
        if (result)
            return Ok("Child removed from parent successfully.");

        return BadRequest("Failed to remove child from parent.");
    }

}