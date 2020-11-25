using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamQualifications")]
    public class ExamQualification : LookupItem
    {
        // TODO: Populate Data

        [Column(Order = 3)] 
        public string JcQualificationCode { get; set; }

        public virtual ICollection<ExamQualificationLevel> Levels { get; set; }
        public virtual ICollection<ExamAward> Awards { get; set; }
    }
}
