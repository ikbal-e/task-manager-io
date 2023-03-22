using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagerIO.API.ValueObjects;

namespace TaskManagerIO.API.Infrastructure.Filters;

public class AuthorizedForAttribute : TypeFilterAttribute
{
    public AuthorizedForAttribute(UserRole userRole) : base(typeof(UserRoleFilter))
    {
        Arguments = new object[] { userRole };
    }

    public class UserRoleFilter : IAuthorizationFilter
    {
        private readonly UserRole _userRole;

        public UserRoleFilter(UserRole userRole)
        {
            _userRole = userRole;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (_userRole == UserRole.Admin)
            {
                var roleClaims = context.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).ToList();

                if (!roleClaims.Select(x => x.Value).Contains("admin")) // TODO: do not hardcode
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
