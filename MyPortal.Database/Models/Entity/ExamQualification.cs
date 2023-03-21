using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamQualifications")]
    public class ExamQualification : LookupItem, ISystemEntity
    {
        // TODO: Populate Data

        [Column(Order = 4)] 
        public string JcQualificationCode { get; set; }

        [Column(Order = 5)]
        public bool System { get; set; }

        public virtual ICollection<ExamQualificationLevel> Levels { get; set; }
        public virtual ICollection<ExamAward> Awards { get; set; }
    }
}
