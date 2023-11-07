using System.Collections.Generic;

namespace MyPortal.Logic.Models.Permissions
{
    internal class SystemArea
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<SystemArea> ChildAreas { get; set; }
        public ICollection<Permission> Permissions { get; set; }

        public SystemArea(string id, string name)
        {
            Id = id;
            Name = name;

            ChildAreas = new List<SystemArea>();
            Permissions = new List<Permission>();
        }
    }
}