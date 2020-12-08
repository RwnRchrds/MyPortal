using System;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamAssessmentModel : BaseModel
    {
        public Guid ExamBoardId { get; set; }
        
        public int AssessmentType { get; set; }
        
        public string InternalTitle { get; set; }
        
        public string ExternalTitle { get; set; }

        public virtual ExamBoardModel ExamBoard { get; set; }
        public virtual ExamAwardModel ExamAward { get; set; }
        public virtual ExamBaseComponentModel ExamBaseComponent { get; set; }
        public virtual ExamBaseElementModel ExamBaseElement { get; set; }
    }
}