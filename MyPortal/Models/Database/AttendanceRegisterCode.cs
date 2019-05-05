namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attendance_RegisterCodes")]
    public partial class AttendanceRegisterCode
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public int MeaningId { get; set; }

        public virtual AttendanceRegisterCodeMeaning AttendanceRegisterCodeMeaning { get; set; }
    }
}
