using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("LogNotes")]
    public class LogNote : BaseTypes.Entity, ISoftDeleteEntity, ICreatable
    {
        [Column(Order = 2)]
        public Guid TypeId { get; set; }

        [Column(Order = 3)]
        public Guid CreatedById { get; set; }

        [Column(Order = 4)]
        public Guid StudentId { get; set; }

        [Column(Order = 5)]
        public Guid AcademicYearId { get; set; }

        [Column(Order = 6)]
        [Required]
        public string Message { get; set; }

        [Column(Order = 7)]
        public DateTime CreatedDate { get; set; }
        
        // Only visible to staff users
        [Column(Order = 8)] 
        public bool Private { get; set; }

        [Column(Order = 9)]
        public bool Deleted { get; set; }

        public virtual User CreatedBy { get; set; }

        public virtual Student Student { get; set; }
            
        public virtual AcademicYear AcademicYear { get; set; }

        public virtual LogNoteType LogNoteType { get; set; }
    }
}
