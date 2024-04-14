using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManagementService.Models;
using UserManagementService.Services;

namespace UserManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            var createdUser = await _userService.CreateAsync(user);
            return CreatedAtAction(nameof(GetAllActive), new { id = createdUser.id }, createdUser);
        }

        [HttpPut("{id}/active")]
        public async Task<IActionResult> UpdateActive(Guid id, [FromBody] UserUpdateDto updateDto)
        {
            var updatedUser = await _userService.UpdateAsync(id, updateDto);
            if (updatedUser == null)
                throw new NotFoundException($"User with ID {id} not found.");

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _userService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            var activeUsers = await _userService.GetAllActiveAsync();
            return Ok(activeUsers);
        }
    }
}