using System.ComponentModel;

namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A staff member in the system.
    /// </summary>
    [Table("People_Staff")]
    public class StaffMember
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StaffMember()
        {
            BehaviourAchievements = new HashSet<BehaviourAchievement>();
            BehaviourIncidents = new HashSet<BehaviourIncident>();
            CurriculumClasses = new HashSet<CurriculumClass>();
            CurriculumLessonPlans = new HashSet<CurriculumLessonPlan>();
            CurriculumSubjects = new HashSet<CurriculumSubject>();
            Documents = new HashSet<Document>();
            MedicalEvents = new HashSet<MedicalEvent>();
            PastoralHouses = new HashSet<PastoralHouse>();
            PastoralRegGroups = new HashSet<PastoralRegGroup>();
            PastoralYearGroups = new HashSet<PastoralYearGroup>();
            ProfileLogs = new HashSet<ProfileLog>();
            PersonnelObservationsOwn = new HashSet<PersonnelObservation>();
            PersonnelObservationsObserved = new HashSet<PersonnelObservation>();
            PersonnelTrainingCertificates = new HashSet<PersonnelTrainingCertificate>();
            Bulletins = new HashSet<SystemBulletin>();
        }

        public int Id { get; set; }

        public int PersonId { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(128)]
        public string Code { get; set; }

        [StringLength(128)]
        public string NiNumber { get; set; }

        [StringLength(128)]
        public string PostNominal { get; set; }

        [DefaultValue(false)]
        public bool TeachingStaff { get; set; }

        public bool Deleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BehaviourAchievement> BehaviourAchievements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BehaviourIncident> BehaviourIncidents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurriculumClass> CurriculumClasses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurriculumLessonPlan> CurriculumLessonPlans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurriculumSubject> CurriculumSubjects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Document> Documents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MedicalEvent> MedicalEvents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PastoralHouse> PastoralHouses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PastoralRegGroup> PastoralRegGroups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PastoralYearGroup> PastoralYearGroups { get; set; }

        public virtual Person Person { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileLog> ProfileLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonnelObservation> PersonnelObservationsOwn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonnelObservation> PersonnelObservationsObserved { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonnelTrainingCertificate> PersonnelTrainingCertificates { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SystemBulletin> Bulletins { get; set; }
    }
}
