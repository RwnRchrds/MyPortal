using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class YearGroupModel : BaseModel
    {
        public YearGroupModel(YearGroup yearGroup)
        {
            StudentGroupId = yearGroup.StudentGroupId;
            CurriculumYearGroupId = yearGroup.CurriculumYearGroupId;
        }
        
        public Guid StudentGroupId { get; set; }
        
        public Guid CurriculumYearGroupId { get; set; }

        public virtual StudentGroupModel StudentGroup { get; set; }
        public virtual CurriculumYearGroupModel CurriculumYearGroup { get; set; }
    }
}