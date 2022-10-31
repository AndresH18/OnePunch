using Microsoft.AspNetCore.Authorization;
using OnePunchApi.Data.Model;

namespace OnePunchApi.Policies.Requirements;

public class RankRequirement : IAuthorizationRequirement
{
    public Rank Rank { get; }

    public RankRequirement(Rank rank)
    {
        Rank = rank;
    }
}