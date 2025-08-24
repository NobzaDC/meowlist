using Xunit;
using MeowCore.Data;
using MeowCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Test.Data
{
    public class ListsRepositoryTests
    {
        private MeowDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MeowDbContext>()
                .UseInMemoryDatabase(databaseName: "ListsTestDb")
                .Options;
            var context = new MeowDbContext(options);

            // Seed mock data if needed
            context.Lists.Add(new Lists { Id = 1, Title = "Test List" });
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task GetListsAsync_ReturnsList()
        {
            var context = GetDbContext();
            var repo = new ListsRepository(context);
            var result = await repo.GetListsAsync();
            Assert.IsType<List<Lists>>(result);
            Assert.NotEmpty(result);
        }
    }
}