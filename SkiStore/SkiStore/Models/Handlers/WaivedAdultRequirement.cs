using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SkiStore.Models.Handlers
{
    public class WaivedAdultRequirement : AuthorizationHandler<WaivedAdultRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WaivedAdultRequirement requirement)
        {
            if (!context.User.HasClaim(claim => claim.Type == ClaimTypes.DateOfBirth)
             || !context.User.HasClaim(claim => claim.Type == "AgreedToWaiver"))
                return Task.CompletedTask;

            //
            return Task.CompletedTask;

        }
    }
}
