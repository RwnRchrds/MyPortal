using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Models.List;

namespace MyPortal.Logic.Models.Entity
{
    public class LogNoteModel
    {
        public Guid Id { get; set; }

        [NotEmpty]
        public Guid TypeId { get; set; }

        public Guid CreatedById { get; set; }

        public Guid UpdatedById { get; set; }

        [NotEmpty]
        public Guid StudentId { get; set; }

        public Guid AcademicYearId { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool Deleted { get; set; }

        public virtual UserModel CreatedBy { get; set; }

        public virtual UserModel UpdatedBy { get; set; }

        public virtual StudentModel Student { get; set; }

        public virtual AcademicYearModel AcademicYear { get; set; }

        public virtual LogNoteTypeModel LogNoteType { get; set; }

        public LogNoteListModel ToListModel()
        {
            return new LogNoteListModel(this);
        }
    }
}
