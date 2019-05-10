namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("People_Staff")]
    public partial class StaffMember
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StaffMember()
        {
            Documents = new HashSet<Document>();
            StaffDocuments = new HashSet<StaffDocument>();
            CurriculumClasses = new HashSet<CurriculumClass>();
            CurriculumLessonPlansAuthored = new HashSet<CurriculumLessonPlan>();
            ProfileLogsWritten = new HashSet<ProfileLog>();
            PastoralRegGroups = new HashSet<PastoralRegGroup>();
            PersonnelObservationsOwn = new HashSet<PersonnelObservation>();
            PersonnelObservationsObserved = new HashSet<PersonnelObservation>();
            CurriculumSubjectsLeading = new HashSet<CurriculumSubject>();
            PersonnelTrainingCertificates = new HashSet<PersonnelTrainingCertificate>();
            PastoralYearGroups = new HashSet<PastoralYearGroup>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [StringLength(255)]
        public string JobTitle { get; set; }

        [EmailAddress]
        [StringLength(320)]
        public string Email { get; set; }

        [Phone]
        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Document> Documents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffDocument> StaffDocuments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurriculumClass> CurriculumClasses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurriculumLessonPlan> CurriculumLessonPlansAuthored { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileLog> ProfileLogsWritten { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PastoralRegGroup> PastoralRegGroups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonnelObservation> PersonnelObservationsOwn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonnelObservation> PersonnelObservationsObserved { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurriculumSubject> CurriculumSubjectsLeading { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonnelTrainingCertificate> PersonnelTrainingCertificates { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PastoralYearGroup> PastoralYearGroups { get; set; }
    }
}
