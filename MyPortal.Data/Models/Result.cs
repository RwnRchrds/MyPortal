using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    /// <summary>
    /// A result a student has achieved for a particular subject.
    /// </summary>
    [Table("Result", Schema = "assessment")]
    public class Result
    {
        public int Id { get; set; }

        public int ResultSetId { get; set; }

        public int StudentId { get; set; }

        public int AspectId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int GradeId { get; set; }

        public virtual ResultSet ResultSet { get; set; }

        public virtual Aspect Aspect { get; set; }

        public virtual Student Student { get; set; }

        public virtual Grade Grade { get; set; }
    }
}
