using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamSeriesModel : BaseModel
    {
        public Guid ExamBoardId { get; set; }
        
        public Guid ExamSeasonId { get; set; }
        
        public string SeriesCode { get; set; }
        
        public string Code { get; set; }
        
        public string Title { get; set; }

        public virtual ExamSeasonModel Season { get; set; }
        public virtual ExamBoardModel ExamBoard { get; set; }
    }
}