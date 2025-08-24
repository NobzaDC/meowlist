using Xunit;
using MeowCore.Data;
using MeowCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Test.Data
{
    public class TagsRepositoryTests
    {
        private MeowDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MeowDbContext>()
                .UseInMemoryDatabase(databaseName: "TagsTestDb")
                .Options;
            var context = new MeowDbContext(options);

            context.Tags.Add(new Tags { Id = 1, Name = "Test Tag" });
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task GetTagsAsync_ReturnsList()
        {
            var context = GetDbContext();
            var repo = new TagsRepository(context);
            var result = await repo.GetTagsAsync();
            Assert.IsType<List<Tags>>(result);
            Assert.NotEmpty(result);
        }
    }
}