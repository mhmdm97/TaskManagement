using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TaskManagementApi.Modules.Interfaces
{
    public interface ITokenModule
    {
        JwtSecurityToken CreateToken(List<Claim> authClaims);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
