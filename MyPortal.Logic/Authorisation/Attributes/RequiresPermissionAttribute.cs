using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Authorisation.Filters;
using MyPortal.Logic.Constants;

namespace MyPortal.Logic.Authorisation.Attributes
{
    public class RequiresPermissionAttribute : TypeFilterAttribute
    {
        public RequiresPermissionAttribute(params int[] permissions) : base(typeof(PermissionsFilter))
        {
            Arguments = new object[] {permissions};
        }
    }
}
