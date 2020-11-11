using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class HomeworkSubmissionModel : BaseModel
    {
        public Guid HomeworkId { get; set; }
        public Guid StudentId { get; set; }
        public Guid TaskId { get; set; }
        public Guid? DocumentId { get; set; }
        public int MaxPoints { get; set; }
        public int PointsAchieved { get; set; }
        public string Comments { get; set; }

        public virtual HomeworkModel HomeworkItem { get; set; }
        public virtual StudentModel Student { get; set; }
        public virtual TaskModel Task { get; set; }
        public virtual DirectoryModel SubmittedWork { get; set; }
    }
}
