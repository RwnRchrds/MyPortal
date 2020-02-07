using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("ProfileLogNote")]
    public partial class ProfileLogNote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid TypeId { get; set; }

        public Guid AuthorId { get; set; }

        public Guid StudentId { get; set; }

        public Guid AcademicYearId { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime Date { get; set; }

        public bool Deleted { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual Student Student { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual ProfileLogNoteType ProfileLogNoteType { get; set; }
    }
}
