using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementService.Models;

namespace UserManagementService.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid id);
        Task<PagedResult<User>> GetAllAsync(bool? active, string? searchTerm, DateTime? initialBirthdate, DateTime? finalBirthdate, string? sort, string? order, int? page, int? size);
        Task<User?> GetByIdAsync(Guid id);
    }
}