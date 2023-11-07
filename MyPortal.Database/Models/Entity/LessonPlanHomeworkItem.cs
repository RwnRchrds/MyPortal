using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("LessonPlanHomeworkItems")]
    public class LessonPlanHomeworkItem : BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid LessonPlanId { get; set; }

        [Column(Order = 3)] public Guid HomeworkItemId { get; set; }

        public virtual LessonPlan LessonPlan { get; set; }
        public virtual HomeworkItem HomeworkItem { get; set; }
    }
}