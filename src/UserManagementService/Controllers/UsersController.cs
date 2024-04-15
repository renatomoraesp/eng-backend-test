using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManagementService.Models;
using UserManagementService.Services;
using UserManagementService.DTOs;

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
            return CreatedAtAction(nameof(GetAll), new { id = createdUser.id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActive(Guid id, [FromBody] UserUpdateDto updateDto)
        {
            var updatedUser = await _userService.UpdateAsync(id, updateDto);
            if (updatedUser == null)
                return NotFound();

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

        [HttpGet]
        public async Task<IActionResult> GetAll(bool? active, string? searchTerm, DateTime? initialBirthdate, DateTime? finalBirthdate, string? sort, string? order)
        {
            var users = await _userService.GetAllAsync(active, searchTerm, initialBirthdate, finalBirthdate, sort, order);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var user = await _userService.GetOneAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}