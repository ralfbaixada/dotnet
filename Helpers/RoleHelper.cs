using System.Linq;
using System.Security.Claims;

namespace Helpers
{
    public static class RoleHelper
    {
        public static bool CheckRole(string role, ClaimsPrincipal user)
        {
            var roles = user.Claims
                .Where(c => c.Type == "Roles")
                .Select(c => c.Value)
                .ToList();

            return roles.Contains(role);
        }
    }
}
