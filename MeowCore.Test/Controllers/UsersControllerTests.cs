using Xunit;
using Moq;
using MeowCore.Controllers;
using MeowCore.Service.Interfaces;
using MeowCore.Models;
using MeowCore.Helpers;
using MeowCore.Helpers.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Test.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<IUsersService> _serviceMock;
        private readonly Mock<IJwtService> _jwtServiceMock;
        private readonly Mock<ILogger<UsersController>> _loggerMock;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _serviceMock = new Mock<IUsersService>();
            _jwtServiceMock = new Mock<IJwtService>();
            _loggerMock = new Mock<ILogger<UsersController>>();
            _controller = new UsersController(_serviceMock.Object, _jwtServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetUsersAsync_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetUsersAsync()).ReturnsAsync(new List<Users> { new Users { Id = 1, Name = "Test" } });
            var result = await _controller.GetUsersAsync();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<List<Users>>>(okResult.Value);
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenUserNotFound()
        {
            _serviceMock.Setup(s => s.ValidateLoginAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Users)null);
            var model = new AuthenticationModel { email = "fail@mail.com", password = "fail" };
            var result = await _controller.Login(model);
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsOk_WhenUserFound()
        {
            _serviceMock.Setup(s => s.ValidateLoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Users { Id = 1, Email = "ok@mail.com" });
            _jwtServiceMock.Setup(j => j.GenerateToken("1", "ok@mail.com"))
                .Returns(new JwtTokenResponse { access_token = "token", token_type = "Bearer", expires_in = 3600 });

            var model = new AuthenticationModel { email = "ok@mail.com", password = "pass" };
            var result = await _controller.Login(model);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<object>>(okResult.Value);
            Assert.True(response.IsSuccess);
            var token = Assert.IsType<JwtTokenResponse>(response.Value);
            Assert.Equal("token", token.access_token);
        }
    }
}