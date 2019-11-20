namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public enum AttendanceMeaning
    {
        Present,
        AuthorisedAbsence,
        ApprovedEducationalActivity,
        UnauthorisedAbsence,
        AttendanceNotRequired,
        NoMark
    }

    /// <summary>
    /// [READ-ONLY] Codes available to use when taking the register.
    /// </summary>
    [Table("Attendance_Codes")]
    public partial class AttendanceCode
    {
        //THIS IS A SYSTEM CLASS AND SHOULD NOT HAVE FEATURES TO ADD, MODIFY OR DELETE DATABASE OBJECTS
        public int Id { get; set; }

        [Required]
        [StringLength(1)]
        public string Code { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public AttendanceMeaning Meaning { get; set; }

        public bool DoNotUse { get; set; }
    }
}
