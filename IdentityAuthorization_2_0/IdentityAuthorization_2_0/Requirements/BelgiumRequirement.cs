using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace IdentityAuthorization_2_0.Requirements
{
    public class BelgiumRequirement : AuthorizationHandler<BelgiumRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            BelgiumRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
                context.Succeed(requirement);

            if (context.User.HasClaim(claim => claim.Type == ClaimTypes.Country && claim.Value == "Belgium"))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}