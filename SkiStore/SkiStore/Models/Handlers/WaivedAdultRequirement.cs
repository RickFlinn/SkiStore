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
            if (!context.User.HasClaim(claim => claim.Type == "DateOfBirth")
             || !context.User.HasClaim(claim => claim.Type == "AgreedToWaiver"))
                return Task.CompletedTask;

            // TODO: logic for policy
            var dateOfBirth = Convert.ToDateTime(
            context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth));

            int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
            {
                calculatedAge--;
            }

            bool acceptedWaiver = Convert.ToBoolean(context.User.FindFirst(c => c.Type == "AgreedToWaiver").Value);

            if (calculatedAge >= 18 && acceptedWaiver)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;

        }
    }
}
