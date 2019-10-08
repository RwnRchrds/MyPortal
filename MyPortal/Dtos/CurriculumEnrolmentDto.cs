namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Represents a student enrolled in a class.
    /// </summary>
    
    public partial class CurriculumEnrolmentDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ClassId { get; set; }

        public virtual CurriculumClassDto CurriculumClass { get; set; }

        public virtual StudentDto Student { get; set; }
    }
}
