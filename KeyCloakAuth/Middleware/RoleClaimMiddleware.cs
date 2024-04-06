using System.Collections.Generic;
using System.Security.Claims;

namespace KeyCloakAuth.Middleware
{
    public class RoleClaimMiddleware
    {
        private readonly RequestDelegate _next;

        public RoleClaimMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.Name != null)
            {
                var identity = (ClaimsIdentity)context.User.Identity;
                //var roleClaim = new Claim(ClaimTypes.Role, "admin", ClaimValueTypes.String, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
                var roleClaim = context.User.FindAll(ClaimTypes.Role).ToList();
                roleClaim.ForEach(x => identity.AddClaim(x));
                //identity.AddClaim(roleClaim);
            }

            await _next(context);
        }
    }
}
