using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace KeyCloakAuth.Handler
{
    public class KeycloakRoleRequirement:IAuthorizationRequirement
    {
        public string Role { get; set; }
        public KeycloakRoleRequirement(string role)
        {
            Role = role;
        }

    }
    public class KeycloakRoleHandler : AuthorizationHandler<KeycloakRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, KeycloakRoleRequirement requirement)
        {
            context.User.FindAll(c => c.Type == ClaimTypes.Role).ToList();
            if (context.User != null && context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == requirement.Role))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
