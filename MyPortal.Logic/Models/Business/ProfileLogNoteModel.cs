using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Business
{
    public class ProfileLogNoteModel
    {
        public Guid Id { get; set; }

        [NotEmpty]
        public Guid TypeId { get; set; }

        public Guid AuthorId { get; set; }

        [NotEmpty]
        public Guid StudentId { get; set; }

        public Guid AcademicYearId { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime Date { get; set; }

        public bool Deleted { get; set; }

        public virtual UserModel Author { get; set; }

        public virtual StudentModel Student { get; set; }

        public virtual AcademicYearModel AcademicYear { get; set; }

        public virtual ProfileLogNoteTypeModel ProfileLogNoteType { get; set; }
    }
}
