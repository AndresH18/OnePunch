using Microsoft.AspNetCore.Authorization;
using Shared.Data.Model;

namespace OnePunch.Api.Security.Policies.Requirements;

public class RankRequirement : IAuthorizationRequirement
{
    public Rank Rank { get; }

    public RankRequirement(Rank rank)
    {
        Rank = rank;
    }
}