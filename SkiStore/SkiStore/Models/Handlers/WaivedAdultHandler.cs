using Microsoft.AspNetCore.Authorization;
using System;
using SkiStore.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SkiStore.Models.Handlers
{
    public class WaivedAdultHandler : AuthorizationHandler<WaivedAdultRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WaivedAdultRequirement requirement)
        {
            if (!context.User.HasClaim(claim => claim.Type == "DOB")
             || !context.User.HasClaim(claim => claim.Type == "AgreedToWaiver"))
                return Task.CompletedTask;

            // TODO: logic for policy
            var dateOfBirth = Convert.ToDateTime(
                context.User.FindFirst(c => c.Type == "DOB").Value);

            int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;

            if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
            {
                calculatedAge--;
            }

            bool acceptedWaiver = Convert.ToBoolean(context.User.FindFirst(c => c.Type == "AgreedToWaiver").Value);
            

            if (calculatedAge >= requirement.MinAge && (requirement.RequiresWaiver ? acceptedWaiver : true))
            {
                context.Succeed(requirement);
            } else
            {
                var authFilterContext = context.Resource as AuthorizationFilterContext;

                string accessDeniedMessage = "Sorry, per company policy users must ";

                if (requirement.RequiresWaiver && !acceptedWaiver)
                {
                    accessDeniedMessage += "sign our product purchase waiver and ";
                }
                
                accessDeniedMessage += $"be at least {requirement.MinAge} to purchase our products."; 

                authFilterContext.Result = new RedirectToActionResult("Index", "Products", accessDeniedMessage);

                context.Succeed(requirement);
            }

            return Task.CompletedTask;

        }
    }
}
