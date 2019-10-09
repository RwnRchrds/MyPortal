namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A period in the week that a class takes place.
    /// </summary>
    [Table("Curriculum_Sessions")]
    public partial class CurriculumSession
    {
        public int Id { get; set; }

        public int ClassId { get; set; }

        public int PeriodId { get; set; }

        public virtual AttendancePeriod AttendancePeriod { get; set; }

        public virtual CurriculumClass Class { get; set; }
    }
}
