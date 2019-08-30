using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models;

namespace MyPortal.ViewModels
{
    public class RolePermissionsViewModel
    {
        public ApplicationRole Role { get; set; }
        public RolePermission RolePermission { get; set; }
    }
}