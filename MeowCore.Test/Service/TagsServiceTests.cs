using Xunit;
using Moq;
using MeowCore.Service;
using MeowCore.Data.Interfaces;
using MeowCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Test.Service
{
    public class TagsServiceTests
    {
        private readonly Mock<ITagsRepository> _repoMock;
        private readonly TagsService _service;

        public TagsServiceTests()
        {
            _repoMock = new Mock<ITagsRepository>();
            _service = new TagsService(_repoMock.Object);
        }

        [Fact]
        public async Task GetTagsAsync_ReturnsTags()
        {
            _repoMock.Setup(r => r.GetTagsAsync()).ReturnsAsync(new List<Tags> { new Tags { Id = 1, Name = "Tag" } });
            var result = await _service.GetTagsAsync();
            Assert.Single(result);
        }
    }
}