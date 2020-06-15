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

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public DayOfWeek Weekday { get; set; }

        [DataMember]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [DataMember]
        [Column(TypeName = "time(2)")]
        public TimeSpan StartTime { get; set; }
        
        [DataMember]
        [Column(TypeName = "time(2)")]
        public TimeSpan EndTime { get; set; }

        [DataMember]
        public bool IsAm { get; set; }

        [DataMember]
        public bool IsPm { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceMark> AttendanceMarks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
