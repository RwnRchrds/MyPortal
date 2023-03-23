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
            LogNotes= new HashSet<LogNote>();
            Achievements = new HashSet<Achievement>();
            Incidents = new HashSet<Incident>();
            AcademicTerms = new HashSet<AcademicTerm>();
        }

        [Column(Order = 2)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 3)] 
        public bool Locked { get; set; }


        
        public virtual ICollection<LogNote> LogNotes { get; set; }
        
        
        public virtual ICollection<Achievement> Achievements { get; set; }

        
        public virtual ICollection<Incident> Incidents { get; set; }

        public virtual ICollection<AcademicTerm> AcademicTerms { get; set; }
    }
}
