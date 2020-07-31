using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ExamAssessmentModes")]
    public class ExamAssessmentMode : LookupItem
    {
        // TODO: Populate Data

        public ExamAssessmentMode()
        {
            Components = new HashSet<ExamComponent>();
        }

        public virtual ICollection<ExamComponent> Components { get; set; }
    }
}
