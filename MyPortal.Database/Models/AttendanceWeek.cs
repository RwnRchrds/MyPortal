using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("AttendanceWeek")]
    public partial class AttendanceWeek
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AttendanceWeek()
        {
            AttendanceMarks = new HashSet<AttendanceMark>();
        }

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid AcademicYearId { get; set; }

        [DataMember]
        [Column(TypeName = "date")]
        public DateTime Beginning { get; set; }

        [DataMember]
        public bool IsHoliday { get; set; }

        [DataMember]
        public bool IsNonTimetable { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceMark> AttendanceMarks { get; set; }
    }
}
