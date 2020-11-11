using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class StaffAbsenceModel : BaseModel
    {
        public Guid StaffMemberId { get; set; }
        
        public Guid AbsenceTypeId { get; set; }
        
        public Guid? IllnessTypeId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public bool AnnualLeave { get; set; }
        
        public bool Confidential { get; set; }
        
        public string Notes { get; set; }

        public virtual StaffMemberModel StaffMember { get; set; }
        public virtual StaffAbsenceTypeModel AbsenceType { get; set; }
        public virtual StaffIllnessTypeModel IllnessType { get; set; }
    }
}