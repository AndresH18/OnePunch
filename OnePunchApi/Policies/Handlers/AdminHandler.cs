using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace OnePunchApi.Policies.Handlers;

public class AdminHandler : AuthorizationHandler<AdminHandler>, IAuthorizationRequirement
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminHandler requirement)
    {
        var userClaims = context.User.Claims;

        var roleClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        if (roleClaim is not null)
        {
            var role = roleClaim.Value;
            if (role == "Administrator")
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}