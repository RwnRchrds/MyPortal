using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Curriculum
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