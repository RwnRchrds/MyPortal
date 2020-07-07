using System;

namespace MyPortal.Logic.Models.Entity
{
    public class RoleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }

        public bool System { get; set; }
    }
}