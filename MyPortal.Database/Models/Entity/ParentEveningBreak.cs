using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models.Entity
{
    [Table("ParentEveningBreaks")]
    public class ParentEveningBreak : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid ParentEveningStaffMemberId { get; set; }

        [Column(Order = 2)]
        public DateTime Start { get; set; }

        [Column(Order = 3)]
        public DateTime End { get; set; }

        public virtual ParentEveningStaffMember ParentEveningStaffMember { get; set; }
    }
}
