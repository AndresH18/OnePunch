using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using OnePunchApi.Policies.Requirements;

namespace OnePunchApi.Policies.Handlers;

public class RoleHandler : AuthorizationHandler<RoleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        var userClaims = context.User.Claims;

        var roleClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        if (roleClaim != null)
        {
            var role = roleClaim.Value;

            if (role == requirement.Role)
                context.Succeed(requirement);
            else 
                context.Fail();
        }

        return Task.CompletedTask;
    }
}