using Xunit;
using MeowCore.Data;
using MeowCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Test.Data
{
    public class TodosRepositoryTests
    {
        private MeowDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MeowDbContext>()
                .UseInMemoryDatabase(databaseName: "TodosTestDb")
                .Options;
            var context = new MeowDbContext(options);

            context.Todos.Add(new Todos { Id = 1, Title = "Test Todo" });
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task GetTodosAsync_ReturnsList()
        {
            var context = GetDbContext();
            var repo = new TodosRepository(context);
            var result = await repo.GetTodosAsync();
            Assert.IsType<List<Todos>>(result);
            Assert.NotEmpty(result);
        }
    }
}