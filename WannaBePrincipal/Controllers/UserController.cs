using Microsoft.AspNetCore.Mvc;
using WannaBePrincipal.Models;

namespace WannaBePrincipal.Controllers
{
    /// <summary>
    /// API controller for managing users.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserModel userModel, ILogger<UserController> logger) : ControllerBase
    {
        private readonly IUserModel _userModel = userModel;
        private readonly ILogger<UserController> _logger = logger;

        /// <summary>
        /// Get a list of all users.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userModel.GetAllUsers();

            _logger.LogInformation("All users queried, there are {usersCount} user in the db.", users.Count);
            return Ok(users);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">The user data to create.</param>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            if (!ModelState.IsValid)  // check body
            {
                _logger.LogWarning("Create user request with invalid model state.");
                return BadRequest(ModelState);
            }

            var newUserId = await _userModel.AddUser(user);
            _logger.LogInformation("User created with ID: {UserId}", newUserId);
            return Created(newUserId, user);
        }

        /// <summary>
        /// Give back one user by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                _logger.LogWarning("Edit GET user request with no id.");
                return BadRequest("Please provide the user id.");
            }
            var user = await _userModel.GetUser(id);
            if (user != null)
            {
                _logger.LogInformation("Edit GET request for this user: {UserId}", id);
                return Ok(user);
            }
            else
            {
                _logger.LogWarning("Edit GET user request with invalid user id.");
                return NotFound("User not found.");
            }
        }

        /// <summary>
        /// Edit an existing user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to edit.</param>
        /// <param name="user">The user data to update.</param>
        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] User user)
        {
            if (id == null)
            {
                _logger.LogWarning("Edit POST user request with no id.");
                return BadRequest("Please provide the user id.");
            }

            if (!ModelState.IsValid) // check body
            {
                _logger.LogWarning("Edit POST user request with invalid model state.");
                return BadRequest(ModelState);
            }

            if (!await _userModel.EditUser(id, user))
            {
                _logger.LogWarning("Edit POST user request with invalid user id.");
                return NotFound("Problem occurred while editing the " + id + " user.");
            }
            _logger.LogInformation("Edit POST request for this user: {UserId}", id);
            return Ok(id + " user was edited.");
        }

        /// <summary>
        /// Delete a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                _logger.LogWarning("Delete user request with no id.");
                return BadRequest("Please provide the user id.");
            }
            if (!await _userModel.DeleteUser(id))
            {
                _logger.LogWarning("Delete user request with invalid user id.");
                return NotFound("Problem occurred while editing the " + id + " user.");
            }
            _logger.LogInformation("Delete request for this user: {UserId}", id);
            return NoContent();
        }
    }
}
