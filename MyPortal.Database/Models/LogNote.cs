using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("LogNote")]
    public partial class LogNote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid TypeId { get; set; }

        public Guid CreatedById { get; set; }

        public Guid UpdatedById { get; set; }

        public Guid StudentId { get; set; }

        public Guid AcademicYearId { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool Deleted { get; set; }

        public virtual ApplicationUser CreatedBy { get; set; }

        public virtual ApplicationUser UpdatedBy { get; set; }

        public virtual Student Student { get; set; }
            
        public virtual AcademicYear AcademicYear { get; set; }

        public virtual LogNoteType LogNoteType { get; set; }
    }
}
