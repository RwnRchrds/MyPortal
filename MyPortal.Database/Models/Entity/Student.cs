using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Students")]
    public class Student : BaseTypes.Entity, ISoftDeleteEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            Results = new HashSet<Result>();
            AttendanceMarks = new HashSet<AttendanceMark>();
            StudentAchievements = new HashSet<StudentAchievement>();
            StudentIncidents = new HashSet<StudentIncident>();
            FinanceBasketItems = new HashSet<BasketItem>();
            Bills = new HashSet<Bill>();
            SenEvents = new HashSet<SenEvent>();
            SenProvisions = new HashSet<SenProvision>();
            ProfileLogs = new HashSet<LogNote>();
            GiftedTalentedSubjects = new HashSet<GiftedTalented>();
            StudentContacts = new HashSet<StudentContactRelationship>();
            HomeworkSubmissions = new HashSet<HomeworkSubmission>();
            Detentions = new HashSet<StudentDetention>();
        }

        [Column(Order = 2)] public Guid PersonId { get; set; }

        [Column(Order = 3)] public int AdmissionNumber { get; set; }

        [Column(Order = 4, TypeName = "date")] public DateTime? DateStarting { get; set; }

        [Column(Order = 5, TypeName = "date")] public DateTime? DateLeaving { get; set; }

        [Column(Order = 6)] public bool FreeSchoolMeals { get; set; }

        [Column(Order = 7)] public Guid? SenStatusId { get; set; }

        [Column(Order = 8)] public Guid? SenTypeId { get; set; }

        [Column(Order = 9)] public Guid? EnrolmentStatusId { get; set; }

        [Column(Order = 10)] public Guid? BoarderStatusId { get; set; }

        [Column(Order = 11)] public bool PupilPremium { get; set; }

        [Column(Order = 12)]
        [StringLength(13)]
        public string Upn { get; set; }

        [Column(Order = 13)] public bool Deleted { get; set; }

        public virtual Person Person { get; set; }

        public virtual SenStatus SenStatus { get; set; }

        public virtual SenType SenType { get; set; }

        public virtual EnrolmentStatus EnrolmentStatus { get; set; }

        public virtual BoarderStatus BoarderStatus { get; set; }


        public virtual ICollection<Result> Results { get; set; }


        public virtual ICollection<AttendanceMark> AttendanceMarks { get; set; }


        public virtual ICollection<StudentAchievement> StudentAchievements { get; set; }


        public virtual ICollection<StudentIncident> StudentIncidents { get; set; }


        public virtual ICollection<BasketItem> FinanceBasketItems { get; set; }


        public virtual ICollection<Bill> Bills { get; set; }


        public virtual ICollection<SenEvent> SenEvents { get; set; }


        public virtual ICollection<SenProvision> SenProvisions { get; set; }

        public virtual ICollection<SenReview> SenReviews { get; set; }

        public virtual ICollection<StudentContactRelationship> StudentContacts { get; set; }


        public virtual ICollection<LogNote> ProfileLogs { get; set; }


        public virtual ICollection<GiftedTalented> GiftedTalentedSubjects { get; set; }


        public virtual ICollection<HomeworkSubmission> HomeworkSubmissions { get; set; }

        public virtual ICollection<StudentAgentRelationship> AgentRelationships { get; set; }

        public virtual ICollection<ReportCard> ReportCards { get; set; }

        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }

        public virtual ICollection<StudentCharge> Charges { get; set; }

        public virtual ICollection<StudentChargeDiscount> ChargeDiscounts { get; set; }
        public virtual ICollection<Exclusion> Exclusions { get; set; }

        public virtual ICollection<ParentEveningAppointment> ParentEveningAppointments { get; set; }

        public virtual ICollection<StudentGroupMembership> StudentGroupMemberships { get; set; }

        public virtual ICollection<ExamCandidate> ExamCandidates { get; set; }

        public virtual ICollection<SessionExtraName> SessionExtraNames { get; set; }

        public virtual ICollection<StudentDetention> Detentions { get; set; }
    }
}