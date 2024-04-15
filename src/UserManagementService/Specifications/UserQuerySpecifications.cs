using System.Linq.Expressions;
using UserManagementService.Models;

namespace UserManagementService.Specifications
{
    public class UserQuerySpecifications
    {
        public static Expression<Func<User, bool>> IsActive(bool active) =>
            u => u.active == active;

        public static Expression<Func<User, bool>> ContainsSearchTerm(string searchTerm) =>
            u => u.name.Contains(searchTerm) || (u.active ? "active" : "inactive").Contains(searchTerm);

        public static Expression<Func<User, bool>> IsBirthdateInRange(DateTime? initial, DateTime? final)
        {
            if (initial.HasValue && final.HasValue)
                return u => u.birthdate >= initial.Value && u.birthdate <= final.Value;
            if (initial.HasValue)
                return u => u.birthdate >= initial.Value;
            if (final.HasValue)
                return u => u.birthdate <= final.Value;
            return u => true;
        }
    }
}