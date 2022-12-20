using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Share_Music.Models;
using System.Security.Claims;

namespace Share_Music.Common.CustomAttribute
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> roles;

        public AuthorizeAttribute(params Role[] roles)
        {
            this.roles = roles ?? Array.Empty<Role>();
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            // authorization based on roles
            Role userRole;

            if (Enum.TryParse(context.HttpContext.User.FindFirstValue(ClaimTypes.Role), out userRole) == false
                ||
                (roles.Any() && !roles.Contains(userRole))
                )
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
