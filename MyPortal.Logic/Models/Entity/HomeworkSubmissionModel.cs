using System;

namespace MyPortal.Logic.Models.Entity
{
    public class HomeworkSubmissionModel
    {
        public Guid Id { get; set; }
        public Guid HomeworkId { get; set; }
        public Guid StudentId { get; set; }
        public Guid TaskId { get; set; }
        public Guid DocumentId { get; set; }
        public int MaxPoints { get; set; }
        public int PointsAchieved { get; set; }
        public string Comments { get; set; }

        public virtual DocumentModel SubmittedWork { get; set; }
        public virtual HomeworkModel Homework { get; set; }
        public virtual StudentModel Student { get; set; }
        public virtual TaskModel Task { get; set; }
    }
}
