using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AuthorizationLab
{
    public class HasTemporaryPassHandler : AuthorizationHandler<OfficeEntryRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OfficeEntryRequirement requirement)
        {
            if(!context.User.HasClaim(c => c.Type == "TemporaryBadgeExpiry" && c.Issuer == "https://contoso.com"))
            {
                return Task.FromResult(0);
            }

            var tempBadgeExpiryTime = Convert.ToDateTime(context.User.FindFirst(c => c.Type == "TemporaryBadgeExpiry" && c.Issuer == "https://contoso.com").Value);
            if(tempBadgeExpiryTime > DateTime.Now)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
