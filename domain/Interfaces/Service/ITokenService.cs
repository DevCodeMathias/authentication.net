using System.Security.Claims;

namespace API_AUTENTICATION.domain.Interfaces.Service
{
    public interface ITokenService
    {
        string GenerateToken(string email);

        ClaimsPrincipal ValidateToken(string token);
    }
}
