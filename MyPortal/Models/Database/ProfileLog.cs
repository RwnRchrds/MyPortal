namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Profile_Logs")]
    public partial class ProfileLog
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public int AuthorId { get; set; }

        public int StudentId { get; set; }

        public int AcademicYearId { get; set; }

        [Required]
        public string Message { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public virtual StaffMember Author { get; set; }

        public virtual Student CoreStudent { get; set; }

        public virtual CurriculumAcademicYear CurriculumAcademicYear { get; set; }

        public virtual ProfileLogType ProfileLogType { get; set; }
    }
}
