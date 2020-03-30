using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Details
{
    public class ProfileLogNoteDetails
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

        public virtual UserDetails Author { get; set; }

        public virtual StudentDetails Student { get; set; }

        public virtual AcademicYearDetails AcademicYear { get; set; }

        public virtual ProfileLogNoteTypeDetails ProfileLogNoteType { get; set; }
    }
}
