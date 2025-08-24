using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MeowCore.Models;

namespace MeowCore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Returns health status and current timestamp.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(HealthStatusModel), 200)]
        public async Task<IActionResult> Get()
        {
            var model = new HealthStatusModel
            {
                Status = "Healthy",
                Timestamp = DateTime.UtcNow
            };
            return Ok(model);
        }
    }
}
