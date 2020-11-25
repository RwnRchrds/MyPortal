using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("StaffAbsences")]
    public class StaffAbsence : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid StaffMemberId { get; set; }

        [Column(Order = 2)]
        public Guid AbsenceTypeId { get; set; }

        [Column(Order = 3)]
        public Guid? IllnessTypeId { get; set; }

        [Column(Order = 4)]
        public DateTime StartDate { get; set; }

        [Column(Order = 5)]
        public DateTime EndDate { get; set; }

        [Column(Order = 6)]
        public bool AnnualLeave { get; set; }

        [Column(Order = 7)]
        public bool Confidential { get; set; }

        [Column(Order = 8)]
        public string Notes { get; set; }

        public virtual StaffMember StaffMember { get; set; }
        public virtual StaffAbsenceType AbsenceType { get; set; }
        public virtual StaffIllnessType IllnessType { get; set; }
    }
}
