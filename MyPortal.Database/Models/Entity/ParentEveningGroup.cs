using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    public class ParentEveningGroup : BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid ParentEveningId { get; set; }

        [Column(Order = 3)] public Guid StudentGroupId { get; set; }

        public virtual ParentEvening ParentEvening { get; set; }
        public virtual StudentGroup StudentGroup { get; set; }
    }
}