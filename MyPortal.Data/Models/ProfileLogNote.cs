using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    /// <summary>
    /// A log note for a student.
    /// </summary>
    [Table("ProfileLogNote")]
    public partial class ProfileLogNote
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public int AuthorId { get; set; }

        public int StudentId { get; set; }

        public int AcademicYearId { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime Date { get; set; }

        public bool Deleted { get; set; }

        public virtual StaffMember Author { get; set; }

        public virtual Student Student { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual ProfileLogNoteType ProfileLogNoteType { get; set; }
    }
}
