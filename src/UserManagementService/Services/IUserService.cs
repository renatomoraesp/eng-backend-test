using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementService.Models;
using UserManagementService.DTOs;

namespace UserManagementService.Services
{
    public interface IUserService
    {
        Task<User> CreateAsync(User user);
        Task<User?> UpdateAsync(Guid id, UserUpdateDto userUpdate);
        Task<bool> DeleteAsync(Guid id);
        Task<PagedResult<User>> GetAllAsync(bool? active, string? searchTerm, DateTime? initialBirthdate, DateTime? finalBirthdate, string? sort, string? order, int? page, int? size);
        Task<User?> GetOneAsync(Guid id);
    }
}