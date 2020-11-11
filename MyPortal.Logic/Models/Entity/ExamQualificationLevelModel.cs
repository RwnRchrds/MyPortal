using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamQualificationLevelModel : LookupItemModel
    {
        public Guid QualificationId { get; set; }
        
        public Guid? DefaultGradeSetId { get; set; }
        
        [StringLength(25)]
        public string JcLevelCode { get; set; }

        public virtual GradeSetModel DefaultGradeSet { get; set; }
        public virtual ExamQualificationModel Qualification { get; set; }
    }
}