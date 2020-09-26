using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("AspNetUsers")]
    public class User : IdentityUser<Guid>, IEntity
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            LogNotesCreated = new HashSet<LogNote>();
            LogNotesUpdated = new HashSet<LogNote>();
            Documents = new HashSet<Document>();
            MedicalEvents = new HashSet<MedicalEvent>();
            Incidents = new HashSet<Incident>();
            Achievements = new HashSet<Achievement>();
            LessonPlans = new HashSet<LessonPlan>();
            Bulletins = new HashSet<Bulletin>();
            AssignedBy = new HashSet<Task>();
            ReportCardSubmissions = new HashSet<ReportCardSubmission>();
        }

        [Column(Order = 3)]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 4)]
        public Guid? PersonId { get; set; }

        [Column(Order = 5)]
        [StringLength(1)]
        public int UserType { get; set; }

        [Column(Order = 6)]
        public bool Enabled { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<LogNote> LogNotesCreated { get; set; }

        public virtual ICollection<LogNote> LogNotesUpdated { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        public virtual ICollection<MedicalEvent> MedicalEvents { get; set; }

        public virtual ICollection<Incident> Incidents { get; set; }

        public virtual ICollection<Achievement> Achievements { get; set; }

        public virtual ICollection<LessonPlan> LessonPlans { get; set; }

        public virtual ICollection<Bulletin> Bulletins { get; set; }

        public virtual ICollection<Task> AssignedBy { get; set; }

        public virtual ICollection<ReportCardSubmission> ReportCardSubmissions { get; set; }

        public virtual ICollection<UserClaim> UserClaims { get; set; }

        public virtual ICollection<UserLogin> UserLogins { get; set; }

        public virtual ICollection<UserToken> UserTokens { get; set; }
    }
}
