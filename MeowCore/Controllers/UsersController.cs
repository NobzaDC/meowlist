using MeowCore.Service.Interfaces;
using MeowCore.Models;
using MeowCore.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MeowCore.Helpers.Interfaces;

namespace MeowCore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _service;
        private readonly IJwtService _jwtService;
        private readonly ILogger<UsersController> _logger;
        private readonly LogHelper<object, ApiResponse<List<Users>>> _logHelperList;
        private readonly LogHelper<int, ApiResponse<Users>> _logHelperGet;
        private readonly LogHelper<Users, ApiResponse<Users>> _logHelperUser;
        private readonly LogHelper<object, ApiResponse<Users>> _logHelperPwd;
        private readonly LogHelper<int, ApiResponse<bool>> _logHelperBool;
        private readonly LogHelper<object, ApiResponse<object>> _logHelperLogin;

        public UsersController(IUsersService service, IJwtService jwtService, ILogger<UsersController> logger)
        {
            _service = service;
            _jwtService = jwtService;
            _logger = logger;
            _logHelperList = new LogHelper<object, ApiResponse<List<Users>>>();
            _logHelperList.CreateLogger(_logger);
            _logHelperGet = new LogHelper<int, ApiResponse<Users>>();
            _logHelperGet.CreateLogger(_logger);
            _logHelperUser = new LogHelper<Users, ApiResponse<Users>>();
            _logHelperUser.CreateLogger(_logger);
            _logHelperPwd = new LogHelper<object, ApiResponse<Users>>();
            _logHelperPwd.CreateLogger(_logger);
            _logHelperBool = new LogHelper<int, ApiResponse<bool>>();
            _logHelperBool.CreateLogger(_logger);
            _logHelperLogin = new LogHelper<object, ApiResponse<object>>();
            _logHelperLogin.CreateLogger(_logger);
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>List of users without passwords.</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<List<Users>>), 200)]
        public async Task<IActionResult> GetUsersAsync()
        {
            var log = _logHelperList.GetInitialLog(
                nameof(UsersController),
                nameof(GetUsersAsync),
                new List<Dictionary<string, string>>(),
                null
            );

            var users = await _service.GetUsersAsync();
            var response = ApiResponse<List<Users>>.Success(users);
            _logHelperList.LogUpdated(log, response, 200);

            return Ok(response);
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>User without password.</returns>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<Users>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Users>), 404)]
        public async Task<IActionResult> GetUsersById(int id)
        {
            var log = _logHelperGet.GetInitialLog(
                nameof(UsersController),
                nameof(GetUsersById),
                new List<Dictionary<string, string>> { new Dictionary<string, string> { { "id", id.ToString() } } },
                id
            );

            var user = await _service.GetUsersById(id);
            int statusCode = user == null ? 404 : 200;
            var response = user == null
                ? ApiResponse<Users>.Fail("User not found", 404)
                : ApiResponse<Users>.Success(user);
            _logHelperGet.LogUpdated(log, response, statusCode);

            if (user == null)
                return NotFound(response);
            return Ok(response);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">User object.</param>
        /// <param name="password">Password in plain text.</param>
        /// <returns>Created user without password.</returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<Users>), 201)]
        [ProducesResponseType(typeof(ApiResponse<Users>), 400)]
        public async Task<IActionResult> CreateUserAsync([FromBody] Users user, [FromQuery] string password)
        {
            // Do not log the password
            var safeUser = user == null ? null : new Users
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                IsAdmin = user.IsAdmin
            };

            var log = _logHelperUser.GetInitialLog(
                nameof(UsersController),
                nameof(CreateUserAsync),
                new List<Dictionary<string, string>>(),
                safeUser
            );

            if (user == null || string.IsNullOrWhiteSpace(password))
            {
                var response = ApiResponse<Users>.Fail("User and password are required.", 400);
                _logHelperUser.LogError(log, 400, "User and password are required.", "", "", nameof(UsersController), nameof(CreateUserAsync));
                return BadRequest(response);
            }

            var createdUser = await _service.CreateUserAsync(user, password);
            var apiResponse = ApiResponse<Users>.Success(createdUser, "User created", 201);
            _logHelperUser.LogUpdated(log, apiResponse, 201);

            return CreatedAtAction(nameof(GetUsersById), new { id = createdUser.Id }, apiResponse);
        }

        /// <summary>
        /// Update user data.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <param name="user">Updated user object.</param>
        /// <returns>Updated user without password.</returns>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<Users>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Users>), 400)]
        [ProducesResponseType(typeof(ApiResponse<Users>), 404)]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] Users user)
        {
            // Do not log the password
            var safeUser = user == null ? null : new Users
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                IsAdmin = user.IsAdmin
            };

            var log = _logHelperUser.GetInitialLog(
                nameof(UsersController),
                nameof(UpdateUserAsync),
                new List<Dictionary<string, string>> { new Dictionary<string, string> { { "id", id.ToString() } } },
                safeUser
            );

            var updatedUser = await _service.UpdateUserAsync(id, user);
            int statusCode = updatedUser == null ? 404 : 200;
            var response = updatedUser == null
                ? ApiResponse<Users>.Fail("User not found", 404)
                : ApiResponse<Users>.Success(updatedUser, "User updated");
            _logHelperUser.LogUpdated(log, response, statusCode);

            if (updatedUser == null)
                return NotFound(response);
            return Ok(response);
        }

        /// <summary>
        /// Update user password.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <param name="password">New password in plain text.</param>
        /// <returns>Updated user without password.</returns>
        [HttpPut("{id}/password")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<Users>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Users>), 400)]
        [ProducesResponseType(typeof(ApiResponse<Users>), 404)]
        public async Task<IActionResult> UpdatePasswordAsync(int id, [FromQuery] string password)
        {
            var log = _logHelperPwd.GetInitialLog(
                nameof(UsersController),
                nameof(UpdatePasswordAsync),
                new List<Dictionary<string, string>> { new Dictionary<string, string> { { "id", id.ToString() } } },
                null // Never log the password
            );

            if (string.IsNullOrWhiteSpace(password))
            {
                var response = ApiResponse<Users>.Fail("Password is required.", 400);
                _logHelperPwd.LogError(log, 400, "Password is required.", "", "", nameof(UsersController), nameof(UpdatePasswordAsync));
                return BadRequest(response);
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var updatedUser = await _service.UpdatePasswordAsync(id, passwordHash);
            int statusCode = updatedUser == null ? 404 : 200;
            var responseOk = updatedUser == null
                ? ApiResponse<Users>.Fail("User not found", 404)
                : ApiResponse<Users>.Success(updatedUser, "Password updated");
            _logHelperPwd.LogUpdated(log, responseOk, statusCode);

            if (updatedUser == null)
                return NotFound(responseOk);
            return Ok(responseOk);
        }

        /// <summary>
        /// Delete user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>True if deleted.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 404)]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var log = _logHelperBool.GetInitialLog(
                nameof(UsersController),
                nameof(DeleteUserAsync),
                new List<Dictionary<string, string>> { new Dictionary<string, string> { { "id", id.ToString() } } },
                id
            );

            var result = await _service.DeleteUserAsync(id);
            int statusCode = result ? 200 : 404;
            var response = result
                ? ApiResponse<bool>.Success(result, "User deleted")
                : ApiResponse<bool>.Fail("User not found", 404);
            _logHelperBool.LogUpdated(log, response, statusCode);

            if (!result)
                return NotFound(response);
            return Ok(response);
        }

        /// <summary>
        /// Login and get JWT token.
        /// </summary>
        /// <param name="model">
        /// AuthenticationModel containing:
        /// <list type="bullet">
        /// <item><description>email: User email.</description></item>
        /// <item><description>password: User password.</description></item>
        /// </list>
        /// </param>
        /// <returns>
        /// <para>Returns <see cref="ApiResponse{object}"/> with JWT token if login is successful.</para>
        /// <para>HTTP 200: Success, returns token.</para>
        /// <para>HTTP 401: Invalid credentials.</para>
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 401)]
        public async Task<IActionResult> Login([FromBody] AuthenticationModel model)
        {
            var log = _logHelperLogin.GetInitialLog(
                nameof(UsersController),
                nameof(Login),
                new List<Dictionary<string, string>>(),
                new { model.email }
            );

            var user = await _service.ValidateLoginAsync(model.email, model.password);
            if (user == null)
            {
                var response = ApiResponse<object>.Fail("Invalid credentials", 401);
                _logHelperLogin.LogError(log, 401, "Invalid credentials.", "", "", nameof(UsersController), nameof(Login));
                return Unauthorized(response);
            }

            var jwtResponse = _jwtService.GenerateToken(user.Id.ToString(), user.Email ?? "");
            var responseOk = ApiResponse<object>.Success(jwtResponse, "Login successful");
            _logHelperLogin.LogUpdated(log, responseOk, 200);

            return Ok(responseOk);
        }
    }
}
