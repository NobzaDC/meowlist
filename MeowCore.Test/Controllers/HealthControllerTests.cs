using Xunit;
using MeowCore.Controllers;
using MeowCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MeowCore.Test.Controllers
{
    public class HealthControllerTests
    {
        [Fact]
        public async Task Get_ReturnsOkWithHealthStatusModel()
        {
            var controller = new HealthController();
            var result = await controller.Get();
            var okResult = Assert.IsType<OkObjectResult>(result);

            var value = Assert.IsType<HealthStatusModel>(okResult.Value);
            Assert.Equal("Healthy", value.Status);
            Assert.True(value.Timestamp <= System.DateTime.UtcNow);
        }
    }
}