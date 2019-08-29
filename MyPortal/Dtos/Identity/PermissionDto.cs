using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos.Identity
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}