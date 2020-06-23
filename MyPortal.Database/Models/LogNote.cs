using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("LogNote")]
    public class LogNote
    {
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid TypeId { get; set; }

        [Column(Order = 2)]
        public Guid CreatedById { get; set; }

        [Column(Order = 3)]
        public Guid UpdatedById { get; set; }

        [Column(Order = 4)]
        public Guid StudentId { get; set; }

        [Column(Order = 5)]
        public Guid AcademicYearId { get; set; }

        [Column(Order = 6)]
        [Required]
        public string Message { get; set; }

        [Column(Order = 7)]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 8)]
        public DateTime UpdatedDate { get; set; }

        [Column(Order = 9)]
        public bool Deleted { get; set; }

        public virtual ApplicationUser CreatedBy { get; set; }

        public virtual ApplicationUser UpdatedBy { get; set; }

        public virtual Student Student { get; set; }
            
        public virtual AcademicYear AcademicYear { get; set; }

        public virtual LogNoteType LogNoteType { get; set; }
    }
}
