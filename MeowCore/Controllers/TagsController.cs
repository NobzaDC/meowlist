using MeowCore.Service.Interfaces;
using MeowCore.Models;
using MeowCore.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeowCore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagsService _service;
        private readonly ILogger<TagsController> _logger;
        private readonly LogHelper<object, ApiResponse<List<Tags>>> _logHelperList;
        private readonly LogHelper<int, ApiResponse<Tags>> _logHelperGet;
        private readonly LogHelper<Tags, ApiResponse<Tags>> _logHelperTagObj;
        private readonly LogHelper<int, ApiResponse<bool>> _logHelperBool;

        public TagsController(ITagsService service, ILogger<TagsController> logger)
        {
            _service = service;
            _logger = logger;
            _logHelperList = new LogHelper<object, ApiResponse<List<Tags>>>();
            _logHelperList.CreateLogger(_logger);
            _logHelperGet = new LogHelper<int, ApiResponse<Tags>>();
            _logHelperGet.CreateLogger(_logger);
            _logHelperTagObj = new LogHelper<Tags, ApiResponse<Tags>>();
            _logHelperTagObj.CreateLogger(_logger);
            _logHelperBool = new LogHelper<int, ApiResponse<bool>>();
            _logHelperBool.CreateLogger(_logger);
        }

        /// <summary>
        /// Get all tags.
        /// </summary>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<List<Tags>>), 200)]
        public async Task<IActionResult> GetTagsAsync()
        {
            var log = _logHelperList.GetInitialLog(
                nameof(TagsController),
                nameof(GetTagsAsync),
                new List<Dictionary<string, string>>(),
                null
            );

            var tags = await _service.GetTagsAsync();
            var response = ApiResponse<List<Tags>>.Success(tags);
            _logHelperList.LogUpdated(log, response, 200);

            return Ok(response);
        }

        /// <summary>
        /// Get tag by id.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<Tags>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Tags>), 404)]
        public async Task<IActionResult> GetTagsById(int id)
        {
            var log = _logHelperGet.GetInitialLog(
                nameof(TagsController),
                nameof(GetTagsById),
                new List<Dictionary<string, string>> { new Dictionary<string, string> { { "id", id.ToString() } } },
                id
            );

            var tag = await _service.GetTagsById(id);
            int statusCode = tag == null ? 404 : 200;
            var response = tag == null
                ? ApiResponse<Tags>.Fail("Tag not found", 404)
                : ApiResponse<Tags>.Success(tag);
            _logHelperGet.LogUpdated(log, response, statusCode);

            if (tag == null)
                return NotFound(response);
            return Ok(response);
        }

        /// <summary>
        /// Create a new tag.
        /// </summary>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<Tags>), 201)]
        [ProducesResponseType(typeof(ApiResponse<Tags>), 400)]
        public async Task<IActionResult> CreateTagAsync([FromBody] Tags tag)
        {
            var log = _logHelperTagObj.GetInitialLog(
                nameof(TagsController),
                nameof(CreateTagAsync),
                new List<Dictionary<string, string>>(),
                tag
            );

            if (tag == null || string.IsNullOrWhiteSpace(tag.Name))
            {
                var response = ApiResponse<Tags>.Fail("Tag and name are required.", 400);
                _logHelperTagObj.LogError(log, 400, "Tag and name are required.", "", "", nameof(TagsController), nameof(CreateTagAsync));
                return BadRequest(response);
            }

            var createdTag = await _service.CreateTagAsync(tag);
            var apiResponse = ApiResponse<Tags>.Success(createdTag, "Tag created", 201);
            _logHelperTagObj.LogUpdated(log, apiResponse, 201);

            return CreatedAtAction(nameof(GetTagsById), new { id = createdTag.Id }, apiResponse);
        }

        /// <summary>
        /// Update tag data.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<Tags>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Tags>), 400)]
        [ProducesResponseType(typeof(ApiResponse<Tags>), 404)]
        public async Task<IActionResult> UpdateTagAsync(int id, [FromBody] Tags tag)
        {
            var log = _logHelperTagObj.GetInitialLog(
                nameof(TagsController),
                nameof(UpdateTagAsync),
                new List<Dictionary<string, string>> { new Dictionary<string, string> { { "id", id.ToString() } } },
                tag
            );

            var updatedTag = await _service.UpdateTagAsync(id, tag);
            int statusCode = updatedTag == null ? 404 : 200;
            var response = updatedTag == null
                ? ApiResponse<Tags>.Fail("Tag not found", 404)
                : ApiResponse<Tags>.Success(updatedTag, "Tag updated");
            _logHelperTagObj.LogUpdated(log, response, statusCode);

            if (updatedTag == null)
                return NotFound(response);
            return Ok(response);
        }

        /// <summary>
        /// Delete tag by id.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 404)]
        public async Task<IActionResult> DeleteTagAsync(int id)
        {
            var log = _logHelperBool.GetInitialLog(
                nameof(TagsController),
                nameof(DeleteTagAsync),
                new List<Dictionary<string, string>> { new Dictionary<string, string> { { "id", id.ToString() } } },
                id
            );

            var result = await _service.DeleteTagAsync(id);
            int statusCode = result ? 200 : 404;
            var response = result
                ? ApiResponse<bool>.Success(result, "Tag deleted")
                : ApiResponse<bool>.Fail("Tag not found", 404);
            _logHelperBool.LogUpdated(log, response, statusCode);

            if (!result)
                return NotFound(response);
            return Ok(response);
        }
    }
}