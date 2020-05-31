using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Business
{
    public class GradeResultModel : ResultModel
    {
        public Guid GradeId { get; set; }

        public virtual GradeModel Grade { get; set; }
    }
}
