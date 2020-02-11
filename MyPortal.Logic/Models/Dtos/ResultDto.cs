using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class ResultDto
    {
        public Guid Id { get; set; }

        public Guid ResultSetId { get; set; }

        public Guid StudentId { get; set; }

        public Guid AspectId { get; set; }

        public DateTime Date { get; set; }

        public Guid GradeId { get; set; }

        public decimal Mark { get; set; }

        public virtual ResultSetDto ResultSet { get; set; }

        public virtual AspectDto Aspect { get; set; }

        public virtual StudentDto Student { get; set; }

        public virtual GradeDto Grade { get; set; }
    }
}
