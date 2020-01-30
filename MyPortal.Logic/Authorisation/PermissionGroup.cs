using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Authorisation
{
    public class PermissionGroup
    {
        public string Name { get; set; }
        public List<Resource> Resources { get; set; }
    }
}
