using Microsoft.AspNetCore.Authorization;
using Shared.Data.Model;

namespace OnePunchApi.Security.Policies.Requirements;

public class RankRequirement : IAuthorizationRequirement
{
    public Rank Rank { get; }

    public RankRequirement(Rank rank)
    {
        Rank = rank;
    }
}