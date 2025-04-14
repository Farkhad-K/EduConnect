using EduConnect.Api.Abstractions;
using EduConnect.Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Api.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class TokensForTeachersController(ITokensForTeachersService tokenService) : ControllerBase
{
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateTokenAsync(CancellationToken abortionToken = default)
    {
        var academyId = JwtClaimHelper.GetAcademyIdFromToken(User);

        if (academyId == null)
            return Forbid("You do not have permission to generate a token.");

        var token = await tokenService.GenerateTokenAsync(academyId.Value, abortionToken);
        return Ok(new { TokenForNewTeacher = token });
    }
}