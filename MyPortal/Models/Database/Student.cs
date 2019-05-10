namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("People_Students")]
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            AssessmentResults = new HashSet<AssessmentResult>();
            AttendanceRegisterMarks = new HashSet<AttendanceRegisterMark>();
            StudentDocuments = new HashSet<StudentDocument>();
            FinanceBasketItems = new HashSet<FinanceBasketItem>();
            ProfileLogs = new HashSet<ProfileLog>();
            FinanceSales = new HashSet<FinanceSale>();
            Enrolments = new HashSet<CurriculumClassEnrolment>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        [StringLength(320)]
        [EmailAddress]
        public string Email { get; set; }

        public int RegGroupId { get; set; }

        public int YearGroupId { get; set; }

        [StringLength(10)]
        public string CandidateNumber { get; set; }

        public decimal AccountBalance { get; set; }

        public bool FreeSchoolMeals { get; set; }

        public bool PupilPremium { get; set; }

        public bool GiftedAndTalented { get; set; }

        [StringLength(255)]
        public string MisId { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssessmentResult> AssessmentResults { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceRegisterMark> AttendanceRegisterMarks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentDocument> StudentDocuments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinanceBasketItem> FinanceBasketItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileLog> ProfileLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinanceSale> FinanceSales { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurriculumClassEnrolment> Enrolments { get; set; }

        public virtual PastoralRegGroup PastoralRegGroup { get; set; }

        public virtual PastoralYearGroup PastoralYearGroup { get; set; }
    }
}
