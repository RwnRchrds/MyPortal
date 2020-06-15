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
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid TypeId { get; set; }

        [DataMember]
        public Guid CreatedById { get; set; }

        [DataMember]
        public Guid UpdatedById { get; set; }

        [DataMember]
        public Guid StudentId { get; set; }

        [DataMember]
        public Guid AcademicYearId { get; set; }

        [DataMember]
        [Required]
        public string Message { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public DateTime UpdatedDate { get; set; }

        [DataMember]
        public bool Deleted { get; set; }

        public virtual ApplicationUser CreatedBy { get; set; }

        public virtual ApplicationUser UpdatedBy { get; set; }

        public virtual Student Student { get; set; }
            
        public virtual AcademicYear AcademicYear { get; set; }

        public virtual LogNoteType LogNoteType { get; set; }
    }
}
