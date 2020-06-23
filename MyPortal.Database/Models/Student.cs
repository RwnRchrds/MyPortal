using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("Student")]
    public class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            Results = new HashSet<Result>();
            AttendanceMarks = new HashSet<AttendanceMark>();
            Achievements = new HashSet<Achievement>();
            Incidents = new HashSet<Incident>();
            Enrolments = new HashSet<Enrolment>();
            FinanceBasketItems = new HashSet<BasketItem>();
            Sales = new HashSet<Sale>();
            MedicalEvents = new HashSet<MedicalEvent>();
            SenEvents = new HashSet<SenEvent>();
            SenProvisions = new HashSet<SenProvision>();
            ProfileLogs = new HashSet<LogNote>();
            GiftedTalentedSubjects = new HashSet<GiftedTalented>();
            StudentContacts = new HashSet<StudentContact>();
            HomeworkSubmissions = new HashSet<HomeworkSubmission>();
        }

        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid PersonId { get; set; }

        [Column(Order = 2)]
        public Guid RegGroupId { get; set; }

        [Column(Order = 3)]
        public Guid YearGroupId { get; set; }

        [Column(Order = 4)]
        public Guid? HouseId { get; set; }

        [Column(Order = 5)]
        [StringLength(128)]
        public string CandidateNumber { get; set; }

        [Column(Order = 6)]
        public int AdmissionNumber { get; set; }

        [Column(Order = 7, TypeName = "date")]
        public DateTime? DateStarting { get; set; }

        [Column(Order = 8, TypeName = "date")]
        public DateTime? DateLeaving { get; set; }

        [Column(Order = 9, TypeName = "decimal(10,2)")]
        public decimal AccountBalance { get; set; }

        [Column(Order = 10)]
        public bool FreeSchoolMeals { get; set; }

        [Column(Order = 11)]
        public bool GiftedAndTalented { get; set; }

        [Column(Order = 12)]
        public Guid? SenStatusId { get; set; }

        [Column(Order = 13)]
        public bool PupilPremium { get; set; }

        [Column(Order = 14)]
        [StringLength(13)]
        public string Upn { get; set; }

        [Column(Order = 15)]
        public string Uci { get; set; }

        [Column(Order = 16)]
        public bool Deleted { get; set; }

        public virtual RegGroup RegGroup { get; set; }

        public virtual YearGroup YearGroup { get; set; }

        public virtual Person Person { get; set; }

        public virtual SenStatus SenStatus { get; set; }

        public virtual House House { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceMark> AttendanceMarks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Achievement> Achievements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Incident> Incidents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enrolment> Enrolments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BasketItem> FinanceBasketItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MedicalEvent> MedicalEvents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SenEvent> SenEvents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SenProvision> SenProvisions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentContact> StudentContacts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LogNote> ProfileLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GiftedTalented> GiftedTalentedSubjects { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HomeworkSubmission> HomeworkSubmissions { get; set; }
    }
}
