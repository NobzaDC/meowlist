using MeowCore.Helpers;
using MeowCore.Models;
using MeowCore.Models.RequestDTOs;
using MeowCore.Models.ResponseDTOs;
using MeowCore.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeowCore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodosService _service;
        private readonly ILogger<TodosController> _logger;
        private readonly LogHelper<object, ApiResponse<List<TodosResponseDto>>> _logHelperList;
        private readonly LogHelper<int, ApiResponse<TodosResponseDto>> _logHelperGet;
        private readonly LogHelper<TodoRequestDto, ApiResponse<TodosResponseDto>> _logHelperTodoObj;
        private readonly LogHelper<int, ApiResponse<bool>> _logHelperBool;

        public TodosController(ITodosService service, ILogger<TodosController> logger)
        {
            _service = service;
            _logger = logger;
            _logHelperList = new LogHelper<object, ApiResponse<List<TodosResponseDto>>>();
            _logHelperList.CreateLogger(_logger);
            _logHelperGet = new LogHelper<int, ApiResponse<TodosResponseDto>>();
            _logHelperGet.CreateLogger(_logger);
            _logHelperTodoObj = new LogHelper<TodoRequestDto, ApiResponse<TodosResponseDto>>();
            _logHelperTodoObj.CreateLogger(_logger);
            _logHelperBool = new LogHelper<int, ApiResponse<bool>>();
            _logHelperBool.CreateLogger(_logger);
        }

        /// <summary>
        /// Get all todos.
        /// </summary>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<List<TodosResponseDto>>), 200)]
        public async Task<IActionResult> GetTodosAsync()
        {
            var log = _logHelperList.GetInitialLog(
                nameof(TodosController),
                nameof(GetTodosAsync),
                new List<Dictionary<string, string>>(),
                null
            );

            var todos = await _service.GetTodosAsync();

            var todoDtos = todos?.Select(todo => new TodosResponseDto(todo)).ToList();

            var response = ApiResponse<List<TodosResponseDto>>.Success(todoDtos);
            _logHelperList.LogUpdated(log, response, 200);

            return Ok(response);
        }

        /// <summary>
        /// Get todo by id.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<TodosResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<TodosResponseDto>), 404)]
        public async Task<IActionResult> GetTodosById(int id)
        {
            var log = _logHelperGet.GetInitialLog(
                nameof(TodosController),
                nameof(GetTodosById),
                new List<Dictionary<string, string>> { new Dictionary<string, string> { { "id", id.ToString() } } },
                id
            );

            var todo = await _service.GetTodosById(id);
            int statusCode = todo == null ? 404 : 200;

            var response = todo == null
                ? ApiResponse<TodosResponseDto>.Fail("Todo not found", 404)
                : ApiResponse<TodosResponseDto>.Success(new TodosResponseDto(todo));
            _logHelperGet.LogUpdated(log, response, statusCode);

            if (todo == null)
                return NotFound(response);
            return Ok(response);
        }

        /// <summary>
        /// Create a new todo.
        /// </summary>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<TodosResponseDto>), 201)]
        [ProducesResponseType(typeof(ApiResponse<TodosResponseDto>), 400)]
        public async Task<IActionResult> CreateTodoAsync([FromBody] TodoRequestDto todo)
        {
            var log = _logHelperTodoObj.GetInitialLog(
                nameof(TodosController),
                nameof(CreateTodoAsync),
                new List<Dictionary<string, string>>(),
                todo
            );

            if (todo == null || string.IsNullOrWhiteSpace(todo.Title))
            {
                var response = ApiResponse<TodosResponseDto>.Fail("Todo and title are required.", 400);
                _logHelperTodoObj.LogError(log, 400, "Todo and title are required.", "", "", nameof(TodosController), nameof(CreateTodoAsync));
                return BadRequest(response);
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                var response = ApiResponse<TodosResponseDto>.Fail("UserId not found in token.", 400);
                return BadRequest(response);
            }
            int userId = int.Parse(userIdClaim.Value);

            // Pasa el userId al servicio
            var createdTodo = await _service.CreateTodoAsync(todo, userId);
            var apiResponse = ApiResponse<TodosResponseDto>.Success(new TodosResponseDto(createdTodo), "Todo created", 201);
            _logHelperTodoObj.LogUpdated(log, apiResponse, 201);

            return CreatedAtAction(nameof(GetTodosById), new { id = createdTodo.Id }, apiResponse);
        }

        /// <summary>
        /// Update todo data.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<TodosResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<TodosResponseDto>), 400)]
        [ProducesResponseType(typeof(ApiResponse<TodosResponseDto>), 404)]
        public async Task<IActionResult> UpdateTodoAsync(int id, [FromBody] TodoRequestDto todo)
        {
            var log = _logHelperTodoObj.GetInitialLog(
                nameof(TodosController),
                nameof(UpdateTodoAsync),
                new List<Dictionary<string, string>> { new Dictionary<string, string> { { "id", id.ToString() } } },
                todo
            );

            var updatedTodo = await _service.UpdateTodoAsync(id, todo);
            int statusCode = updatedTodo == null ? 404 : 200;
            var response = updatedTodo == null
                ? ApiResponse<TodosResponseDto>.Fail("Todo not found", 404)
                : ApiResponse<TodosResponseDto>.Success(new TodosResponseDto(updatedTodo), "Todo updated");
            _logHelperTodoObj.LogUpdated(log, response, statusCode);

            if (updatedTodo == null)
                return NotFound(response);
            return Ok(response);
        }

        /// <summary>
        /// Delete todo by id.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 404)]
        public async Task<IActionResult> DeleteTodoAsync(int id)
        {
            var log = _logHelperBool.GetInitialLog(
                nameof(TodosController),
                nameof(DeleteTodoAsync),
                new List<Dictionary<string, string>> { new Dictionary<string, string> { { "id", id.ToString() } } },
                id
            );

            var result = await _service.DeleteTodoAsync(id);
            int statusCode = result ? 200 : 404;
            var response = result
                ? ApiResponse<bool>.Success(result, "Todo deleted")
                : ApiResponse<bool>.Fail("Todo not found", 404);
            _logHelperBool.LogUpdated(log, response, statusCode);

            if (!result)
                return NotFound(response);
            return Ok(response);
        }

        /// <summary>
        /// Get all todos for a specific user.
        /// </summary>
        [HttpGet("user/{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<List<TodosResponseDto>>), 200)]
        public async Task<IActionResult> GetTodosByUser(int userId)
        {
            var log = _logHelperList.GetInitialLog(
                nameof(TodosController),
                nameof(GetTodosByUser),
                new List<Dictionary<string, string>> { new Dictionary<string, string> { { "userId", userId.ToString() } } },
                userId
            );

            var todos = await _service.GetTodosByUser(userId);
            var response = ApiResponse<List<TodosResponseDto>>.Success(todos?.Select(todo => new TodosResponseDto(todo)).ToList());
            _logHelperList.LogUpdated(log, response, 200);

            return Ok(response);
        }

        /// <summary>
        /// Get all todos for a specific list.
        /// </summary>
        [HttpGet("list/{listId}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<List<TodosResponseDto>>), 200)]
        public async Task<IActionResult> GetTodosByList(int listId)
        {
            var log = _logHelperList.GetInitialLog(
                nameof(TodosController),
                nameof(GetTodosByList),
                new List<Dictionary<string, string>> { new Dictionary<string, string> { { "listId", listId.ToString() } } },
                listId
            );

            var todos = await _service.GetTodosByList(listId);
            var response = ApiResponse<List<TodosResponseDto>>.Success(todos?.Select(todo => new TodosResponseDto(todo)).ToList());
            _logHelperList.LogUpdated(log, response, 200);

            return Ok(response);
        }
    }
}