namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A period in the week that a class takes place.
    /// </summary>
    
    public partial class CurriculumSessionDto
    {
        public int Id { get; set; }

        public int ClassId { get; set; }

        public int PeriodId { get; set; }

        public virtual AttendancePeriodDto AttendancePeriod { get; set; }

        public virtual CurriculumClassDto CurriculumClass { get; set; }
    }
}
