using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementService.Models;
using UserManagementService.Repositories;
using UserManagementService.DTOs;

namespace UserManagementService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateAsync(User user)
        {
            if (!user.active)
                user.active = true;
            
            user.birthdate = DateTime.SpecifyKind(user.birthdate, DateTimeKind.Utc);
            return await _userRepository.CreateAsync(user);
        }

        public async Task<User?> UpdateAsync(Guid id, UserUpdateDto userUpdate)
        {
            if (userUpdate.birthdate.HasValue)
                userUpdate.birthdate = DateTime.SpecifyKind(userUpdate.birthdate.Value, DateTimeKind.Utc);
            
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            user.name = userUpdate.name ?? user.name;
            user.birthdate = userUpdate.birthdate ?? user.birthdate;
            user.active = userUpdate.active ?? user.active;

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync(bool? active, string? searchTerm, DateTime? initialBirthdate, DateTime? finalBirthdate, string? sort, string? order)
        {
            if (initialBirthdate.HasValue)
                initialBirthdate = DateTime.SpecifyKind(initialBirthdate.Value, DateTimeKind.Utc);
            
            if (finalBirthdate.HasValue)
                finalBirthdate = DateTime.SpecifyKind(finalBirthdate.Value, DateTimeKind.Utc);
            
            return await _userRepository.GetAllAsync(active, searchTerm, initialBirthdate, finalBirthdate, sort, order);
        }

        public async Task<User?> GetOneAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }
    }
}