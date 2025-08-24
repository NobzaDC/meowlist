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
    public class TodosControllerTests
    {
        private readonly Mock<ITodosService> _serviceMock;
        private readonly Mock<ILogger<TodosController>> _loggerMock;
        private readonly TodosController _controller;

        public TodosControllerTests()
        {
            _serviceMock = new Mock<ITodosService>();
            _loggerMock = new Mock<ILogger<TodosController>>();
            _controller = new TodosController(_serviceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetTodosAsync_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetTodosAsync()).ReturnsAsync(new List<Todos> { new Todos { Id = 1, Title = "Todo1" } });
            var result = await _controller.GetTodosAsync();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<List<Todos>>>(okResult.Value);
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task CreateTodoAsync_ReturnsBadRequest_WhenInvalid()
        {
            var result = await _controller.CreateTodoAsync(null);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}