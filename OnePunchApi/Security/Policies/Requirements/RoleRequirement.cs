using Microsoft.AspNetCore.Authorization;
using OnePunchApi.Data.Model;
using OnePunchApi.Security.Models;

namespace OnePunchApi.Security.Policies.Requirements;

public class RoleRequirement : IAuthorizationRequirement
{
    public Role Role { get; }

    public RoleRequirement(Role role)
    {
        Role = role;
    }
}