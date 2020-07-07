using System;

namespace MyPortal.Logic.Models.Entity
{
    public class GradeResultModel : ResultModel
    {
        public Guid GradeId { get; set; }

        public virtual GradeModel Grade { get; set; }
    }
}
