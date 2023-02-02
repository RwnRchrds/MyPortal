using System.Collections;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Settings
{
    public class RoleModel : BaseModel
    {
        public RoleModel(Role model) : base(model)
        {
            Name = model.Name;
            ConcurrencyStamp = model.ConcurrencyStamp;
            NormalizedName = model.NormalizedName;
            Description = model.Description;
            Permissions = model.Permissions;
            System = model.System;
        }
        
        public string Name { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }
        public byte[] Permissions { get; set; }
        public bool System { get; set; }

        public bool HasPermission(int permission)
        {
            return PermissionArray[permission];
        }

        public BitArray PermissionArray
        {
            get
            {
                return new BitArray(Permissions);
            }
        }
    }
}