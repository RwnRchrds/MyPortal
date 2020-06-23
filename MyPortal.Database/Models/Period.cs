using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("AttendancePeriod")]
    public partial class Period
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Period()
        {
            AttendanceMarks = new HashSet<AttendanceMark>();
            Sessions = new HashSet<Session>();
        }

        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public DayOfWeek Weekday { get; set; }

        [Column(Order = 2)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 3, TypeName = "time(2)")]
        public TimeSpan StartTime { get; set; }
        
        [Column(Order = 4, TypeName = "time(2)")]
        public TimeSpan EndTime { get; set; }

        [Column(Order = 5)]
        public bool IsAm { get; set; }

        [Column(Order = 6)]
        public bool IsPm { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceMark> AttendanceMarks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
