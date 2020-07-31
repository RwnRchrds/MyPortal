using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ExamQualificationLevels")]
    public class ExamQualificationLevel : LookupItem
    {
        // TODO: Populate Data

        [Column(Order = 3)]
        public Guid QualificationId { get; set; }

        public virtual ExamQualification Qualification { get; set; }
        public virtual ICollection<Course> Courses { get; set; }   
    }
}
