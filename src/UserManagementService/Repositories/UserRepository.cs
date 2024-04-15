using Microsoft.EntityFrameworkCore;
using UserManagementService.Models;
using UserManagementService.Specifications;
using UserManagementService.Strategies;

namespace UserManagementService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;

        public UserRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
                return false;

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetAllAsync(bool? active, string? searchTerm, DateTime? initialBirthdate, DateTime? finalBirthdate, string? sort, string? order)
        {
            IQueryable<User> query = _dbContext.Users;

            if (active.HasValue)
                query = query.Where(UserQuerySpecifications.IsActive(active.Value));

            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(UserQuerySpecifications.ContainsSearchTerm(searchTerm));

            query = query.Where(UserQuerySpecifications.IsBirthdateInRange(initialBirthdate, finalBirthdate));

            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order))
                query = UserSortingStrategies.ApplySorting(query, sort, order);

            return await query.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
    }
}