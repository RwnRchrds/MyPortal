using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class ProfileLogNoteDto
    {
        public Guid Id { get; set; }

        public Guid TypeId { get; set; }

        public Guid AuthorId { get; set; }

        public Guid StudentId { get; set; }

        public Guid AcademicYearId { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime Date { get; set; }

        public bool Deleted { get; set; }

        public virtual StaffMemberDto Author { get; set; }

        public virtual StudentDto Student { get; set; }

        public virtual AcademicYearDto AcademicYear { get; set; }

        public virtual ProfileLogNoteTypeDto ProfileLogNoteType { get; set; }
    }
}
