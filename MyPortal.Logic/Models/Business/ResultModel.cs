using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Business
{
    public abstract class ResultModel
    {
        public Guid Id { get; set; }

        public Guid ResultSetId { get; set; }

        public Guid StudentId { get; set; }

        public Guid AspectId { get; set; }

        public DateTime Date { get; set; }

        public string Comments { get; set; }

        public virtual ResultSetModel ResultSet { get; set; }

        public virtual AspectModel Aspect { get; set; }

        public virtual StudentModel Student { get; set; }

        public Type GetResultType()
        {
            if (this is GradeResultModel)
            {
                return typeof(GradeResultModel);
            }

            if (this is NumericResultModel)
            {
                return typeof(NumericResultModel);
            }

            throw new Exception("Could not determine result type.");
        }
    }
}
