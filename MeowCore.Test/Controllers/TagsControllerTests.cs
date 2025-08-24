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
    public class TagsControllerTests
    {
        private readonly Mock<ITagsService> _serviceMock;
        private readonly Mock<ILogger<TagsController>> _loggerMock;
        private readonly TagsController _controller;

        public TagsControllerTests()
        {
            _serviceMock = new Mock<ITagsService>();
            _loggerMock = new Mock<ILogger<TagsController>>();
            _controller = new TagsController(_serviceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetTagsAsync_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetTagsAsync()).ReturnsAsync(new List<Tags> { new Tags { Id = 1, Name = "Tag1" } });
            var result = await _controller.GetTagsAsync();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<List<Tags>>>(okResult.Value);
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task CreateTagAsync_ReturnsBadRequest_WhenInvalid()
        {
            var result = await _controller.CreateTagAsync(null);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}