using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class SubjectStaffMemberRoleModel : LookupItemModel
    {
        public SubjectStaffMemberRoleModel(SubjectStaffMemberRole model) : base(model)
        {
            SubjectLeader = model.SubjectLeader;
        }
        
        public bool SubjectLeader { get; set; }
    }
}