using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class AttendanceWeekPatternModel
    {
        public Guid Id { get; set; }
        
        public Guid AcademicYearId { get; set; }
        
        public int Order { get; set; }
        
        [Required]
        [StringLength(128)]
        public string Description { get; set; }
        
        public virtual AcademicYearModel AcademicYear { get; set; }
    }
}