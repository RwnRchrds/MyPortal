namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// [SYSTEM] Timetable period definitions for each week.
    /// </summary>
    public partial class AttendancePeriodDto
    {
        //THIS IS A SYSTEM CLASS AND SHOULD NOT HAVE FEATURES TO ADD, MODIFY OR DELETE DATABASE OBJECTS
        public int Id { get; set; }

        public string Weekday { get; set; }

        public string Name { get; set; }

        [Display(Name="Start Time")]
        public TimeSpan StartTime { get; set; }

        [Display(Name="End Time")]
        public TimeSpan EndTime { get; set; }

        public bool IsAm { get; set; }

        public bool IsPm { get; set; }
    }
}
