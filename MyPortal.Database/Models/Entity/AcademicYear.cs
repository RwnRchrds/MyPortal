using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("AcademicYears")]
    public class AcademicYear : BaseTypes.Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AcademicYear()
        {
            AttendanceWeekPatterns = new HashSet<AttendanceWeekPattern>();
            Classes = new HashSet<Class>();
            LogNotes= new HashSet<LogNote>();
            Achievements = new HashSet<Achievement>();
            Incidents = new HashSet<Incident>();
            AcademicTerms = new HashSet<AcademicTerm>();
        }

        [Column(Order = 1)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 4)] 
        public bool Locked { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceWeekPattern> AttendanceWeekPatterns { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Class> Classes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LogNote> LogNotes { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Achievement> Achievements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Incident> Incidents { get; set; }

        public virtual ICollection<AcademicTerm> AcademicTerms { get; set; }
    }
}
