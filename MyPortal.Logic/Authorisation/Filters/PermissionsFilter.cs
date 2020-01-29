using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Constants;

namespace MyPortal.Logic.Authorisation.Filters
{
    public class PermissionsFilter : IAuthorizationFilter
    {
        private readonly Permission[] _permissions;

        public PermissionsFilter(Permission[] permissions)
        {
            _permissions = permissions;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            foreach (var permission in _permissions)
            {
                var hasClaim =
                    context.HttpContext.User.Claims.Any(x => x.Type == ClaimType.Permissions && x.Value == permission.ToString());

                if (hasClaim)
                {
                    return;
                }
            }

            context.Result = new ForbidResult();
        }
    }
}
