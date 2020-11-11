using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ReportCardTargetSubmissionModel : BaseModel
    {
        public Guid SubmissionId { get; set; }
        
        public Guid TargetId { get; set; }
        
        public bool TargetCompleted { get; set; }

        public virtual ReportCardSubmissionModel Submission { get; set; }
        public virtual ReportCardTargetModel Target { get; set; }
    }
}