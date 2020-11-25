using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("StaffMembers")]
    public class StaffMember : BaseTypes.Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StaffMember()
        {
            Sessions = new HashSet<Session>();
            PastoralHouses = new HashSet<House>();
            PastoralRegGroups = new HashSet<RegGroup>();
            PastoralYearGroups = new HashSet<YearGroup>();
            PersonnelObservations = new HashSet<Observation>();
            PersonnelObservationsObserved = new HashSet<Observation>();
            PersonnelTrainingCertificates = new HashSet<TrainingCertificate>();
            Subjects = new HashSet<SubjectStaffMember>();
            Subordinates = new HashSet<StaffMember>();
            CoverArrangements = new HashSet<CoverArrangement>();
            Absences = new HashSet<StaffAbsence>();
        }

        [Column(Order = 1)]
        public Guid? LineManagerId { get; set; }

        [Column(Order = 2)]
        public Guid PersonId { get; set; }

        [Column(Order = 3)]
        [Required]
        [StringLength(128)]
        public string Code { get; set; }

        [Column(Order = 4)]
        [StringLength(128)]
        public string NiNumber { get; set; }

        [Column(Order = 5)]
        [StringLength(128)]
        public string PostNominal { get; set; }

        [Column(Order = 6)]
        public bool TeachingStaff { get; set; }

        [Column(Order = 7)]
        public bool Deleted { get; set; }

        public virtual Person Person { get; set; }

        public virtual StaffMember LineManager { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Session> Sessions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectStaffMember> Subjects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<House> PastoralHouses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegGroup> PastoralRegGroups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YearGroup> PastoralYearGroups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Observation> PersonnelObservations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Observation> PersonnelObservationsObserved { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrainingCertificate> PersonnelTrainingCertificates { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detention> SupervisedDetentions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffMember> Subordinates { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoverArrangement> CoverArrangements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffAbsence> Absences { get; set; }

        public virtual ICollection<ActivitySupervisor> Activities { get; set; }
    }
}
