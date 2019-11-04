using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Dtos.Identity;
using MyPortal.Interfaces;

namespace MyPortal.Models.Misc
{
    public class PermissionIndicator : IGridDto
    {
        public PermissionDto Permission { get; set; }
        public bool HasPermission { get; set; }
    }
}