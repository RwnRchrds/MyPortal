using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class HomeworkSubmissionDto
    {
        public int Id { get; set; }
        public int HomeworkId { get; set; }
        public int TaskId { get; set; }
        public int MaxPoints { get; set; }
        public int PointsAchieved { get; set; }
        public string Comments { get; set; }

        public virtual HomeworkDto Homework { get; set; }
        public virtual TaskDto Task { get; set; }
    }
}
