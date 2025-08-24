using Xunit;
using Moq;
using MeowCore.Service;
using MeowCore.Data.Interfaces;
using MeowCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Test.Service
{
    public class UsersServiceTests
    {
        private readonly Mock<IUsersRepository> _repoMock;
        private readonly UsersService _service;

        public UsersServiceTests()
        {
            _repoMock = new Mock<IUsersRepository>();
            _service = new UsersService(_repoMock.Object);
        }

        [Fact]
        public async Task GetUsersAsync_ReturnsUsers()
        {
            _repoMock.Setup(r => r.GetUsersAsync()).ReturnsAsync(new List<Users> { new Users { Id = 1, Name = "Test" } });
            var result = await _service.GetUsersAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task ValidateLoginAsync_ReturnsNull_WhenNotFound()
        {
            _repoMock.Setup(r => r.GetUserByEmailAsync("fail@mail.com")).ReturnsAsync((Users)null);
            var result = await _service.ValidateLoginAsync("fail@mail.com", "pass");
            Assert.Null(result);
        }
    }
}