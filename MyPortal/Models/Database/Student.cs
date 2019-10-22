using MyPortal.Models.Attributes;

namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A student in the system.
    /// </summary>
    [Table("People_Students")]
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            AssessmentResults = new HashSet<AssessmentResult>();
            AttendanceRegisterMarks = new HashSet<AttendanceMark>();
            BehaviourAchievements = new HashSet<BehaviourAchievement>();
            BehaviourIncidents = new HashSet<BehaviourIncident>();
            Enrolments = new HashSet<CurriculumEnrolment>();
            FinanceBasketItems = new HashSet<FinanceBasketItem>();
            FinanceSales = new HashSet<FinanceSale>();
            MedicalEvents = new HashSet<MedicalEvent>();
            SenEvents = new HashSet<SenEvent>();
            SenProvisions = new HashSet<SenProvision>();
            ProfileLogs = new HashSet<ProfileLog>();
            GiftedTalentedSubjects = new HashSet<SenGiftedTalented>();
        }

        public int Id { get; set; }

        public int PersonId { get; set; }

        public int RegGroupId { get; set; }

        public int YearGroupId { get; set; }

        public int? HouseId { get; set; }

        [StringLength(128)]
        public string CandidateNumber { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdmissionNumber { get; set; }

        public decimal AccountBalance { get; set; }

        public bool FreeSchoolMeals { get; set; }

        public bool GiftedAndTalented { get; set; }

        public int? SenStatusId { get; set; }

        public bool PupilPremium { get; set; }

        [StringLength(256)]
        public string MisId { get; set; }

        [StringLength(13)]
        [Upn]
        public string Upn { get; set; }

        public string Uci { get; set; }

        public bool Deleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssessmentResult> AssessmentResults { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceMark> AttendanceRegisterMarks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BehaviourAchievement> BehaviourAchievements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BehaviourIncident> BehaviourIncidents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurriculumEnrolment> Enrolments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinanceBasketItem> FinanceBasketItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinanceSale> FinanceSales { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MedicalEvent> MedicalEvents { get; set; }

        public virtual PastoralRegGroup PastoralRegGroup { get; set; }

        public virtual PastoralYearGroup PastoralYearGroup { get; set; }

        public virtual Person Person { get; set; }

        public virtual SenStatus SenStatus { get; set; }

        public virtual PastoralHouse House { get; set; }    

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SenEvent> SenEvents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SenProvision> SenProvisions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileLog> ProfileLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SenGiftedTalented> GiftedTalentedSubjects { get; set; }
    }
}
