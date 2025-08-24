using Xunit;
using Moq;
using MeowCore.Service;
using MeowCore.Data.Interfaces;
using MeowCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Test.Service
{
    public class ListsServiceTests
    {
        private readonly Mock<IListsRepository> _repoMock;
        private readonly ListsService _service;

        public ListsServiceTests()
        {
            _repoMock = new Mock<IListsRepository>();
            _service = new ListsService(_repoMock.Object);
        }

        [Fact]
        public async Task GetListsAsync_ReturnsLists()
        {
            _repoMock.Setup(r => r.GetListsAsync()).ReturnsAsync(new List<Lists> { new Lists { Id = 1, Title = "List" } });
            var result = await _service.GetListsAsync();
            Assert.Single(result);
        }
    }
}