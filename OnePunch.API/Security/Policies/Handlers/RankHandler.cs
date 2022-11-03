using Microsoft.AspNetCore.Authorization;
using OnePunchApi.Security.Policies.Requirements;
using OnePunchApi.Services;

namespace OnePunchApi.Security.Policies.Handlers;

public class RankHandler : AuthorizationHandler<RankRequirement>
{
    private readonly UserManager _userManager;

    public RankHandler(UserManager userManager)
    {
        _userManager = userManager;
    }
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RankRequirement requirement)
    {
        var user = _userManager.GetCurrentUser(context);
        if (user != null)
        {
            if (user.Rank == requirement.Rank)
                context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}