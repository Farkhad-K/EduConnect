using System.Net.Http.Headers;
using EduConnect.Api.Abstractions.ServicesAbstractions;
using EduConnect.Api.Mappers.ParentMappers;
using EduConnect.Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParentsController(
    IParentsService parentsService): ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAllMyChildren(CancellationToken abortionToken = default)
    {
        var parentId = JwtClaimHelper.GetUserIdFromToken(User);
        if (parentId is null) return Forbid("You do not have permission to access this resource.");

        var children = await parentsService.GetChildrenOfParent(parentId.Value, abortionToken);
        
        return Ok(children.ToDto());
    }

    [Authorize(Roles = "Parent")]
    [HttpPost]
    public async ValueTask<IActionResult> AttachStudentToParent([FromBody]Guid studentId, CancellationToken abortionToken = default)
    {
        var parentId = JwtClaimHelper.GetUserIdFromToken(User);
        if (parentId is null) return Forbid("You do not have permission to access this resource.");

        var result = await parentsService.AttachStudentToParent(parentId.Value, studentId, abortionToken);
        if (result)
            return Ok("Child attachet to parent successfully.");

        return BadRequest("Failed to attach child to parent.");      
    }
}