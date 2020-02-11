using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class HomeworkSubmissionDto
    {
        public Guid Id { get; set; }
        public Guid HomeworkId { get; set; }
        public Guid TaskId { get; set; }
        public int MaxPoints { get; set; }
        public int PointsAchieved { get; set; }
        public string Comments { get; set; }

        public virtual HomeworkDto Homework { get; set; }
        public virtual TaskDto Task { get; set; }
    }
}
