using Xunit;
using Moq;
using MeowCore.Service;
using MeowCore.Data.Interfaces;
using MeowCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Test.Service
{
    public class TodosServiceTests
    {
        private readonly Mock<ITodosRepository> _repoMock;
        private readonly TodosService _service;

        public TodosServiceTests()
        {
            _repoMock = new Mock<ITodosRepository>();
            _service = new TodosService(_repoMock.Object);
        }

        [Fact]
        public async Task GetTodosAsync_ReturnsTodos()
        {
            _repoMock.Setup(r => r.GetTodosAsync()).ReturnsAsync(new List<Todos> { new Todos { Id = 1, Title = "Todo" } });
            var result = await _service.GetTodosAsync();
            Assert.Single(result);
        }
    }
}