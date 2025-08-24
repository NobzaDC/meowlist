using Xunit;
using MeowCore.Data;
using MeowCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Test.Data
{
    public class UsersRepositoryTests
    {
        private MeowDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MeowDbContext>()
                .UseInMemoryDatabase(databaseName: "UsersTestDb")
                .Options;
            var context = new MeowDbContext(options);

            context.Users.Add(new Users { Id = 1, Name = "Test User", Email = "test@mail.com" });
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task GetUsersAsync_ReturnsList()
        {
            var context = GetDbContext();
            var repo = new UsersRepository(context);
            var result = await repo.GetUsersAsync();
            Assert.IsType<List<Users>>(result);
        }
    }
}