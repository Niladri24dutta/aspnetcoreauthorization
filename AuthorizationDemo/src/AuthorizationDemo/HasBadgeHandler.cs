﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationLab
{
    public class HasBadgeHandler : AuthorizationHandler<OfficeEntryRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OfficeEntryRequirement requirement)
        {
           if(!context.User.HasClaim( c => c.Type == "BadgeNumber" && c.Issuer == "https://contoso.com"))
            {
                return Task.CompletedTask;

            }
            context.Succeed(requirement);
            return Task.CompletedTask;  
        }
    }
}
