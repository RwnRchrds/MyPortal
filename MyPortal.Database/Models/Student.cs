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

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid PersonId { get; set; }

        [DataMember]
        public Guid RegGroupId { get; set; }

        [DataMember]
        public Guid YearGroupId { get; set; }

        [DataMember]
        public Guid? HouseId { get; set; }

        [DataMember]
        [StringLength(128)]
        public string CandidateNumber { get; set; }

        [DataMember]
        public int AdmissionNumber { get; set; }

        [DataMember]
        [Column(TypeName = "date")]
        public DateTime? DateStarting { get; set; }

        [DataMember]
        [Column(TypeName = "date")]
        public DateTime? DateLeaving { get; set; }

        [DataMember]
        [Column(TypeName = "decimal(10,2)")]
        public decimal AccountBalance { get; set; }

        [DataMember]
        public bool FreeSchoolMeals { get; set; }

        [DataMember]
        public bool GiftedAndTalented { get; set; }

        [DataMember]
        public Guid? SenStatusId { get; set; }

        [DataMember]
        public bool PupilPremium { get; set; }

        [DataMember]
        [StringLength(13)]
        public string Upn { get; set; }

        [DataMember]
        public string Uci { get; set; }

        [DataMember]
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
