using System;

namespace MyPortal.Logic.Models.Dtos
{
    public class ResultDto
    {
        public int Id { get; set; }

        public int ResultSetId { get; set; }

        public int StudentId { get; set; }

        public int AspectId { get; set; }
        public DateTime Date { get; set; }

        public int GradeId { get; set; }

        public virtual ResultSetDto ResultSet { get; set; }

        public virtual AspectDto Aspect { get; set; }

        public virtual StudentDto Student { get; set; }

        public virtual GradeDto Grade { get; set; }
    }
}
