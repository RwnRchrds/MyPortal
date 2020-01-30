using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("HomeworkSubmission")]
    public class HomeworkSubmission
    {
        public int Id { get; set; }
        public int HomeworkId { get; set; }
        public int TaskId { get; set; }
        public int MaxPoints { get; set; }
        public int PointsAchieved { get; set; }
        public string Comments { get; set; }

        public virtual Homework Homework { get; set; }
        public virtual Task Task { get; set; }
    }
}
