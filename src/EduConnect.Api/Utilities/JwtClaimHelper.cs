using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EduConnect.Api.Utilities;

public static class JwtClaimHelper
{
    public static Guid? GetAcademyIdFromToken(ClaimsPrincipal user)
    {
        var claim = user.Claims.FirstOrDefault(c => c.Type == "AcademyId");
        return claim != null ? Guid.Parse(claim.Value) : null;
    }

    public static Guid? GetUserIdFromToken(ClaimsPrincipal user)
    {
        var idClaim = user.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        return Guid.TryParse(idClaim, out var id) ? id : null;
    }
}