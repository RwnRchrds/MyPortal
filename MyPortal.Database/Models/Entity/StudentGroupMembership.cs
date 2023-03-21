using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("StudentGroupMemberships")]
    public class StudentGroupMembership : BaseTypes.Entity
    {
        [Column(Order = 2)]
        public Guid StudentId { get; set; }
        
        [Column(Order = 3)]
        public Guid StudentGroupId { get; set; }
        
        [Column(Order = 4, TypeName = "date")]
        public DateTime StartDate { get; set; }
        
        [Column(Order = 5, TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public virtual Student Student { get; set; }
        public virtual StudentGroup StudentGroup { get; set; }
    }
}