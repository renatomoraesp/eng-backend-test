using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementService.Models;

namespace UserManagementService.Services
{
    public interface IUserService
    {
        Task<User> CreateAsync(User user);
        Task<User?> UpdateAsync(Guid id, UserUpdateDto userUpdate);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<User>> GetAllActiveAsync();
    }
}