namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// [SYSTEM] Codes available to use when taking the register.
    /// </summary>
    [Table("Attendance_RegisterCodes")]
    public partial class AttendanceCode
    {
        //THIS IS A SYSTEM CLASS AND SHOULD NOT HAVE FEATURES TO ADD, MODIFY OR DELETE DATABASE OBJECTS
        public int Id { get; set; }

        [Required]
        [StringLength(1)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Display(Name="Meaning")]
        public int MeaningId { get; set; }

        public bool System { get; set; }

        public virtual AttendanceMeaning AttendanceMeaning { get; set; }
    }
}
