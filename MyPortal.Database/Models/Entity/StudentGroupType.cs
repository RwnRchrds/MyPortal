using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("StudentGroupTypes")]
    public class StudentGroupType : LookupItem
    {
        [Column(Order = 3)]
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
        
        [Column(Order = 4)]
        public bool AllowSimultaneous { get; set; }
        
        [Column(Order = 5)]
        public bool AllowPromotion { get; set; }

        public virtual ICollection<StudentGroup> StudentGroups { get; set; }
    }
}