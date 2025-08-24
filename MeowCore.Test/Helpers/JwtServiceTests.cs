using MeowCore.Helpers;
using MeowCore.Models;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace MeowCore.Test.Helpers
{
    public class JwtServiceTests
    {
        [Fact]
        public void GenerateToken_ReturnsValidJwtTokenResponse()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"Jwt:Key", "supersecretkey12345678901234567890"},
                {"Jwt:ExpireMinutes", "60"}
            };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var jwtService = new JwtService(configuration);
            var result = jwtService.GenerateToken("1", "test@mail.com");

            Assert.NotNull(result);
            Assert.StartsWith("ey", result.access_token);
            Assert.Equal("Bearer", result.token_type);
            Assert.Equal(3600, result.expires_in);
        }
    }
}