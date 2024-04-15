using System.Linq.Expressions;
using UserManagementService.Models;

namespace UserManagementService.Strategies
{
    public class UserSortingStrategies
    {
        private static readonly Dictionary<string, Expression<Func<User, object>>> SortOptions = new Dictionary<string, Expression<Func<User, object>>>()
        {
            { "id", u => u.id },
            { "name", u => u.name },
            { "birthdate", u => u.birthdate },
            { "active", u => u.active }
        };

        public static IQueryable<User> ApplySorting(IQueryable<User> query, string sortKey, string order)
        {
            if (SortOptions.TryGetValue(sortKey.ToLower(), out var sortExpression))
            {
                if (order.ToLower() == "asc")
                    return query.OrderBy(sortExpression);
                else
                    return query.OrderByDescending(sortExpression);
            }
            return query;
        }
    }
}