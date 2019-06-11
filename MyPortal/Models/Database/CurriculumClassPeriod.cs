namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Represents the assignment of a period in the week to a class.
    /// </summary>
    [Table("Curriculum_ClassPeriods")]
    public partial class CurriculumClassPeriod
    {
        public int Id { get; set; }

        public int ClassId { get; set; }

        public int PeriodId { get; set; }

        public virtual AttendancePeriod AttendancePeriod { get; set; }

        public virtual CurriculumClass CurriculumClass { get; set; }
    }
}
