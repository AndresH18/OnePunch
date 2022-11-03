using Microsoft.AspNetCore.Authorization;
using OnePunchApi.Security.Policies.Requirements;
using OnePunchApi.Services;

namespace OnePunchApi.Security.Policies.Handlers;

public class RoleHandler : AuthorizationHandler<RoleRequirement>
{
    private readonly UserManager _userManager;

    public RoleHandler(UserManager userManager)
    {
        _userManager = userManager;
    }

    public override Task HandleAsync(AuthorizationHandlerContext context)
    {
        return base.HandleAsync(context);
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        var user = _userManager.GetCurrentUser(context);
        if (user != null)
        {
            if (user.Role == requirement.Role)
                context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}