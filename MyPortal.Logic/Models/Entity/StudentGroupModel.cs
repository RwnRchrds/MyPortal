using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class StudentGroupModel : BaseModel
    {
        public string Code { get; set; }
        
        public Guid StudentGroupTypeId { get; set; }
        
        public Guid? PromoteToGroupId { get; set; }
        
        public int? MaxMembers { get; set; }
        
        public string Notes { get; set; }
        
        public bool System { get; set; }
        
        public StudentGroupModel PromoteToGroup { get; set; }
        public StudentGroupTypeModel StudentGroupType { get; set; }
    }
}