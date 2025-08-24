using Xunit;
using Moq;
using MeowCore.Controllers;
using MeowCore.Service.Interfaces;
using MeowCore.Models;
using MeowCore.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Test.Controllers
{
    public class ListsControllerTests
    {
        private readonly Mock<IListsService> _serviceMock;
        private readonly Mock<ILogger<ListsController>> _loggerMock;
        private readonly ListsController _controller;

        public ListsControllerTests()
        {
            _serviceMock = new Mock<IListsService>();
            _loggerMock = new Mock<ILogger<ListsController>>();
            _controller = new ListsController(_serviceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetListsAsync_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetListsAsync()).ReturnsAsync(new List<Lists> { new Lists { Id = 1, Title = "List1" } });
            var result = await _controller.GetListsAsync();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<List<Lists>>>(okResult.Value);
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task GetListsById_ReturnsNotFound_WhenNull()
        {
            _serviceMock.Setup(s => s.GetListsById(1)).ReturnsAsync((Lists)null);
            var result = await _controller.GetListsById(1);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task CreateListAsync_ReturnsBadRequest_WhenInvalid()
        {
            var result = await _controller.CreateListAsync(null);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}