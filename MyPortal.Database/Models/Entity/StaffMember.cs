using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("StaffMembers")]
    public class StaffMember : BaseTypes.Entity, ISoftDeleteEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        [Column(Order = 2)] public Guid? LineManagerId { get; set; }

        [Column(Order = 3)] public Guid PersonId { get; set; }

        [Column(Order = 4)]
        [Required]
        [StringLength(128)]
        public string Code { get; set; }

        [Column(Order = 5)] [StringLength(50)] public string BankName { get; set; }

        [Column(Order = 6)] [StringLength(15)] public string BankAccount { get; set; }

        [Column(Order = 7)] [StringLength(10)] public string BankSortCode { get; set; }

        [Column(Order = 8)] [StringLength(9)] public string NiNumber { get; set; }

        [Column(Order = 9)]
        [StringLength(128)]
        public string Qualifications { get; set; }

        [Column(Order = 10)] public bool TeachingStaff { get; set; }

        [Column(Order = 11)] public bool Deleted { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<NextOfKin> NextOfKin { get; set; }

        public virtual StaffMember LineManager { get; set; }


        public virtual ICollection<Session> Sessions { get; set; }


        public virtual ICollection<SubjectStaffMember> Subjects { get; set; }


        public virtual ICollection<House> PastoralHouses { get; set; }


        public virtual ICollection<RegGroup> PastoralRegGroups { get; set; }


        public virtual ICollection<YearGroup> PastoralYearGroups { get; set; }


        public virtual ICollection<Observation> PersonnelObservations { get; set; }


        public virtual ICollection<Observation> PersonnelObservationsObserved { get; set; }


        public virtual ICollection<TrainingCertificate> PersonnelTrainingCertificates { get; set; }


        public virtual ICollection<Detention> SupervisedDetentions { get; set; }


        public virtual ICollection<StaffMember> Subordinates { get; set; }


        public virtual ICollection<CoverArrangement> CoverArrangements { get; set; }


        public virtual ICollection<StaffAbsence> Absences { get; set; }

        public virtual ICollection<ParentEveningStaffMember> ParentEvenings { get; set; }

        public virtual ICollection<StudentGroupSupervisor> StudentGroupSupervisors { get; set; }

        public virtual ICollection<SenReview> SenReviews { get; set; }
    }
}