namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// An individual mark in the register.
    /// </summary>
    
    public partial class AttendanceMarkDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int WeekId { get; set; }

        public int PeriodId { get; set; }

        
        
        public string Mark { get; set; }

        public virtual AttendancePeriodDto Period { get; set; }

        public virtual StudentDto Student { get; set; }

        public virtual AttendanceWeekDto Week { get; set; }
    }
}
