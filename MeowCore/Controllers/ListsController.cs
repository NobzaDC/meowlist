using MeowCore.Service.Interfaces;
using MeowCore.Models;
using MeowCore.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeowCore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        private readonly IListsService _service;
        private readonly ILogger<ListsController> _logger;
        private readonly LogHelper<object, ApiResponse<List<Lists>>> _logHelperList;
        private readonly LogHelper<int, ApiResponse<Lists>> _logHelperGet;
        private readonly LogHelper<Lists, ApiResponse<Lists>> _logHelperListObj;
        private readonly LogHelper<int, ApiResponse<bool>> _logHelperBool;

        public ListsController(IListsService service, ILogger<ListsController> logger)
        {
            _service = service;
            _logger = logger;
            _logHelperList = new LogHelper<object, ApiResponse<List<Lists>>>();
            _logHelperList.CreateLogger(_logger);
            _logHelperGet = new LogHelper<int, ApiResponse<Lists>>();
            _logHelperGet.CreateLogger(_logger);
            _logHelperListObj = new LogHelper<Lists, ApiResponse<Lists>>();
            _logHelperListObj.CreateLogger(_logger);
            _logHelperBool = new LogHelper<int, ApiResponse<bool>>();
            _logHelperBool.CreateLogger(_logger);
        }

        /// <summary>
        /// Get all lists.
        /// </summary>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<List<Lists>>), 200)]
        public async Task<IActionResult> GetListsAsync()
        {
            var log = _logHelperList.GetInitialLog(
                nameof(ListsController),
                nameof(GetListsAsync),
                new List<Dictionary<string, string>>(),
                null
            );

            var lists = await _service.GetListsAsync();
            var response = ApiResponse<List<Lists>>.Success(lists);
            _logHelperList.LogUpdated(log, response, 200);

            return Ok(response);
        }

        /// <summary>
        /// Get list by id.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<Lists>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Lists>), 404)]
        public async Task<IActionResult> GetListsById(int id)
        {
            var log = _logHelperGet.GetInitialLog(
                nameof(ListsController),
                nameof(GetListsById),
                new List<Dictionary<string, string>> { new Dictionary<string, string> { { "id", id.ToString() } } },
                id
            );

            var list = await _service.GetListsById(id);
            int statusCode = list == null ? 404 : 200;
            var response = list == null
                ? ApiResponse<Lists>.Fail("List not found", 404)
                : ApiResponse<Lists>.Success(list);
            _logHelperGet.LogUpdated(log, response, statusCode);

            if (list == null)
                return NotFound(response);
            return Ok(response);
        }

        /// <summary>
        /// Create a new list.
        /// </summary>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<Lists>), 201)]
        [ProducesResponseType(typeof(ApiResponse<Lists>), 400)]
        public async Task<IActionResult> CreateListAsync([FromBody] Lists list)
        {
            var log = _logHelperListObj.GetInitialLog(
                nameof(ListsController),
                nameof(CreateListAsync),
                new List<Dictionary<string, string>>(),
                list
            );

            if (list == null || string.IsNullOrWhiteSpace(list.Title))
            {
                var response = ApiResponse<Lists>.Fail("List and title are required.", 400);
                _logHelperListObj.LogError(log, 400, "List and title are required.", "", "", nameof(ListsController), nameof(CreateListAsync));
                return BadRequest(response);
            }

            var createdList = await _service.CreateListAsync(list);
            var apiResponse = ApiResponse<Lists>.Success(createdList, "List created", 201);
            _logHelperListObj.LogUpdated(log, apiResponse, 201);

            return CreatedAtAction(nameof(GetListsById), new { id = createdList.Id }, apiResponse);
        }

        /// <summary>
        /// Update list data.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<Lists>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Lists>), 400)]
        [ProducesResponseType(typeof(ApiResponse<Lists>), 404)]
        public async Task<IActionResult> UpdateListAsync(int id, [FromBody] Lists list)
        {
            var log = _logHelperListObj.GetInitialLog(
                nameof(ListsController),
                nameof(UpdateListAsync),
                new List<Dictionary<string, string>> { new Dictionary<string, string> { { "id", id.ToString() } } },
                list
            );

            var updatedList = await _service.UpdateListAsync(id, list);
            int statusCode = updatedList == null ? 404 : 200;
            var response = updatedList == null
                ? ApiResponse<Lists>.Fail("List not found", 404)
                : ApiResponse<Lists>.Success(updatedList, "List updated");
            _logHelperListObj.LogUpdated(log, response, statusCode);

            if (updatedList == null)
                return NotFound(response);
            return Ok(response);
        }

        /// <summary>
        /// Delete list by id.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 404)]
        public async Task<IActionResult> DeleteListAsync(int id)
        {
            var log = _logHelperBool.GetInitialLog(
                nameof(ListsController),
                nameof(DeleteListAsync),
                new List<Dictionary<string, string>> { new Dictionary<string, string> { { "id", id.ToString() } } },
                id
            );

            var result = await _service.DeleteListAsync(id);
            int statusCode = result ? 200 : 404;
            var response = result
                ? ApiResponse<bool>.Success(result, "List deleted")
                : ApiResponse<bool>.Fail("List not found", 404);
            _logHelperBool.LogUpdated(log, response, statusCode);

            if (!result)
                return NotFound(response);
            return Ok(response);
        }
    }
}