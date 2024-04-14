using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagementService.Models;

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
            {
                query = query.Where(u => u.active == active.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(u => u.name.Contains(searchTerm) || (u.active ? "active" : "inactive").Contains(searchTerm));
            }

            if (initialBirthdate.HasValue && finalBirthdate.HasValue)
            {
                query = query.Where(u => u.birthdate >= initialBirthdate.Value && u.birthdate <= finalBirthdate.Value);
            }
            else if (initialBirthdate.HasValue)
            {
                query = query.Where(u => u.birthdate >= initialBirthdate.Value);
            }
            else if (finalBirthdate.HasValue)
            {
                query = query.Where(u => u.birthdate <= finalBirthdate.Value);
            }

            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order))
            {
                var orderBy = order.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";

                switch (sort.ToLower())
                {
                    case "id":
                        query = query.OrderBy(u => u.id);
                        break;
                    case "name":
                        query = query.OrderBy(u => u.name);
                        break;
                    case "birthdate":
                        query = query.OrderBy(u => u.birthdate);
                        break;
                    case "active":
                        query = query.OrderBy(u => u.active);
                        break;
                    default:
                        break;
                }

                if (orderBy == "OrderByDescending")
                {
                    query = query.Reverse();
                }
            }

            return await query.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
    }
}