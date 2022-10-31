using Microsoft.AspNetCore.Authorization;
using OnePunchApi.Data.Model;
using OnePunchApi.Policies.Requirements;

namespace OnePunchApi.Policies.Handlers;

public class RankHandler : AuthorizationHandler<RankRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RankRequirement requirement)
    {
        var userClaims = context.User.Claims;
        var rankClaim = context.User.Claims.FirstOrDefault(c => c.Type == "Rank");
        if (rankClaim != null)
        {
            if (Enum.TryParse(rankClaim.Value, true, out Rank rank))
            {
                if (rank == requirement.Rank)
                {
                    context.Succeed(requirement);
                }
            }
            else
            {
                context.Fail();
            }
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}