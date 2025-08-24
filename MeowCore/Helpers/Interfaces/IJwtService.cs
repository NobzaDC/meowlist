using MeowCore.Models;

namespace MeowCore.Helpers.Interfaces
{
    /// <summary>
    /// Service for generating JWT tokens. Registered via DI.
    /// </summary>
    public interface IJwtService
    {
        JwtTokenResponse GenerateToken(string userId, string email);
    }
}
