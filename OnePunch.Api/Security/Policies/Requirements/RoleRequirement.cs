using Microsoft.AspNetCore.Authorization;
using OnePunch.Api.Security.Models;

namespace OnePunch.Api.Security.Policies.Requirements;

public class RoleRequirement : IAuthorizationRequirement
{
    public Role Role { get; }

    public RoleRequirement(Role role)
    {
        Role = role;
    }
}