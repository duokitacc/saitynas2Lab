﻿using Microsoft.AspNetCore.Authorization;
using Saitynas1Lab.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas1Lab.Auth
{
    public class SameUserAuthorizationHandler : AuthorizationHandler<SameUserRequirement,IUserOwnedResource>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameUserRequirement requirement, IUserOwnedResource resource)
        {
            
            if (context.User.IsInRole(DemoRestUserRoles.Admin) || context.User.FindFirst(CustomClaims.UserId).Value == resource.UserId)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
    public record SameUserRequirement : IAuthorizationRequirement;
}
