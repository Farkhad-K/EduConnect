using System.IdentityModel.Tokens.Jwt;
using EduConnect.Api.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GetUserAndAcademyIdController : ControllerBase
{
    [HttpGet]
    public IActionResult Get(CancellationToken abortionToken = default)
    {
        // var userId = JwtClaimHelper.GetUserIdFromToken(User);
        var userId = GetUserIdFromToken();
        var academyId = JwtClaimHelper.GetAcademyIdFromToken(User);
        return Ok(new { userId, academyId });
    }

    private Guid? GetUserIdFromToken()
    {
        var idClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        return Guid.TryParse(idClaim, out var id) ? id : null;
    }
}

/*
public static Guid? GetUserIdFromToken(ClaimsPrincipal user)
    {
        var idClaim = user.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        return Guid.TryParse(idClaim, out var id) ? id : null;
    }
*/