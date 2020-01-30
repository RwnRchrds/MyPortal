using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Authorisation
{
    public class Resource
    {
        public string Name { get; set; }
        public List<PermissionItem> Permissions { get; set; }
    }

    public class PermissionItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
    }
}
