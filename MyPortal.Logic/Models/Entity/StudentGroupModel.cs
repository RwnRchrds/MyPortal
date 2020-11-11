using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class StudentGroupModel : BaseModel
    {
        public Guid GroupType { get; set; }
        
        public Guid BaseGroupId { get; set; }
    }
}