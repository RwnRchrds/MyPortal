using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public abstract class ResultModel : BaseModel
    {
        public Guid ResultSetId { get; set; }

        public Guid StudentId { get; set; }

        public Guid AspectId { get; set; }

        public DateTime Date { get; set; }

        public Guid? GradeId { get; set; }

        public decimal? Mark { get; set; }

        public string Comments { get; set; }

        public string Note { get; set; }

        public string ColourCode { get; set; }

        public virtual ResultSetModel ResultSet { get; set; }

        public virtual AspectModel Aspect { get; set; }

        public virtual StudentModel Student { get; set; }

        public virtual GradeModel Grade { get; set; }
    }
}
