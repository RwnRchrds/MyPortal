using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Dtos.Identity;

namespace MyPortal.Models.Misc
{
    public class PermissionIndicator
    {
        public PermissionDto Permission { get; set; }
        public bool HasPermission { get; set; }
    }
}