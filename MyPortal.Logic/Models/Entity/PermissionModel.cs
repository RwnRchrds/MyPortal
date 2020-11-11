using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class PermissionModel : BaseModel
    {
        public Guid AreaId { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public virtual SystemAreaModel SystemArea { get; set; }
    }
}
