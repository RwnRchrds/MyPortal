using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamAssessmentModes")]
    public class ExamAssessmentMode : LookupItem
    {
        // TODO: Populate Data

        [Column(Order = 3)] 
        public bool ExternallyAssessed { get; set; }

        public ExamAssessmentMode()
        {
            Components = new HashSet<ExamComponent>();
        }

        public virtual ICollection<ExamComponent> Components { get; set; }
        public virtual ICollection<ExamBaseComponent> ExamBaseComponents { get; set; }
    }
}
