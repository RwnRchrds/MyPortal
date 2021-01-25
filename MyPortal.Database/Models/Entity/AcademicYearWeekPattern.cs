using System;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("AcademicYearWeekPatterns")]
    public class AcademicYearWeekPattern : BaseTypes.Entity, IOrderedEntity
    {
        [Column(Order = 1)]
        public Guid AcademicYearId { get; set; }

        [Column(Order = 2)] 
        public Guid WeekPatternId { get; set; }

        [Column(Order = 3)]
        public int Order { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }
        public virtual AttendanceWeekPattern AttendanceWeekPattern { get; set; }
    }
}
