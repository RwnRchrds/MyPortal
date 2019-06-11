namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Represents the assignment of a period in the week to a class.
    /// </summary>
    public partial class CurriculumClassPeriodDto
    {
        public int Id { get; set; }

        public int ClassId { get; set; }

        public int PeriodId { get; set; }

        public virtual AttendancePeriodDto AttendancePeriod { get; set; }

        public virtual CurriculumClassDto CurriculumClass { get; set; }
    }
}
