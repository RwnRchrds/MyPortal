using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Dictionaries;
using MyPortal.Logic.Extensions;

namespace MyPortal.Logic.Authorisation.Filters
{
    public class PermissionsFilter : IAuthorizationFilter
    {
        private readonly int[] _permissions;

        public PermissionsFilter(int[] permissions)
        {
            _permissions = permissions;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAllowed = context.HttpContext.User.HasPermission(_permissions);

            if (isAllowed)
            {
                return;
            }

            context.Result = new ForbidResult();
        }
    }
}
