using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    /// <summary>
    /// Represents a student enrolled in a class.
    /// </summary>
    [Table("Enrolment", Schema = "curriculum")]
    public partial class Enrolment
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ClassId { get; set; }

        public virtual Class Class { get; set; }

        public virtual Student Student { get; set; }
    }
}
