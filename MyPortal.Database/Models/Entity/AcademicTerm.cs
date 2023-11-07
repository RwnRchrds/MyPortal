using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("AcademicTerms")]
    public class AcademicTerm : BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid AcademicYearId { get; set; }

        [Column(Order = 3)]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 4, TypeName = "date")] public DateTime StartDate { get; set; }

        [Column(Order = 5, TypeName = "date")] public DateTime EndDate { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }
        public virtual ICollection<AttendanceWeek> AttendanceWeeks { get; set; }
    }
}