using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("Enrolment")]
    public partial class Enrolment
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ClassId { get; set; }

        public virtual Class Class { get; set; }

        public virtual Student Student { get; set; }
    }
}
