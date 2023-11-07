using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ParentEveningBreaks")]
    public class ParentEveningBreak : BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid ParentEveningStaffMemberId { get; set; }

        [Column(Order = 3)] public DateTime Start { get; set; }

        [Column(Order = 4)] public DateTime End { get; set; }

        public virtual ParentEveningStaffMember ParentEveningStaffMember { get; set; }
    }
}