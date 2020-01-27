using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Authorisation.Filters;

namespace MyPortal.Logic.Authorisation.Attributes
{
    public class RequiresPermissionAttribute : TypeFilterAttribute
    {
        public RequiresPermissionAttribute(params string[] permissions) : base(typeof(PermissionsFilter))
        {
            Arguments = new object[] {permissions};
        }
    }
}
