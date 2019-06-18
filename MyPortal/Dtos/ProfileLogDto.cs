namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A log note for a student.
    /// </summary>
    public partial class ProfileLogDto
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public int AuthorId { get; set; }

        public int StudentId { get; set; }

        public int AcademicYearId { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }

        public bool Deleted { get; set; }

        public virtual StaffMemberDto Author { get; set; }

        public virtual StudentDto CoreStudent { get; set; }

        public virtual CurriculumAcademicYearDto CurriculumAcademicYear { get; set; }

        public virtual ProfileLogTypeDto ProfileLogType { get; set; }
    }
}
