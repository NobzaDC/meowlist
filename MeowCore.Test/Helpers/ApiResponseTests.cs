using MeowCore.Helpers;
using Xunit;

namespace MeowCore.Test.Helpers
{
    public class ApiResponseTests
    {
        [Fact]
        public void Success_ShouldReturnSuccessResponse()
        {
            var result = ApiResponse<string>.Success("ok", "msg", 200);
            Assert.True(result.IsSuccess);
            Assert.Equal("ok", result.Value);
            Assert.Equal("msg", result.Message);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public void Fail_ShouldReturnFailResponse()
        {
            var result = ApiResponse<string>.Fail("error", 400);
            Assert.False(result.IsSuccess);
            Assert.Null(result.Value);
            Assert.Equal("error", result.Message);
            Assert.Equal(400, result.Code);
        }
    }
}