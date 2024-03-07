using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Users")]
    public class User : IdentityUser<Guid>, IEntity
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            LogNotesCreated = new HashSet<LogNote>();
            Documents = new HashSet<Document>();
            MedicalEvents = new HashSet<MedicalEvent>();
            Incidents = new HashSet<Incident>();
            Achievements = new HashSet<Achievement>();
            LessonPlans = new HashSet<LessonPlan>();
            Bulletins = new HashSet<Bulletin>();
            CreatedTasks = new HashSet<Task>();
            ReportCardSubmissions = new HashSet<ReportCardEntry>();
        }

        public DateTime CreatedDate { get; set; }

        public Guid? PersonId { get; set; }

        [StringLength(1)] public int UserType { get; set; }

        public bool Enabled { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<Result> Results { get; set; }

        public virtual ICollection<LogNote> LogNotesCreated { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        public virtual ICollection<MedicalEvent> MedicalEvents { get; set; }

        public virtual ICollection<Incident> Incidents { get; set; }

        public virtual ICollection<Achievement> Achievements { get; set; }

        public virtual ICollection<LessonPlan> LessonPlans { get; set; }

        public virtual ICollection<Bulletin> Bulletins { get; set; }

        public virtual ICollection<Task> CreatedTasks { get; set; }

        public virtual ICollection<ReportCardEntry> ReportCardSubmissions { get; set; }

        public virtual ICollection<UserClaim> UserClaims { get; set; }

        public virtual ICollection<UserLogin> UserLogins { get; set; }

        public virtual ICollection<UserToken> UserTokens { get; set; }

        public virtual ICollection<DiaryEvent> DiaryEvents { get; set; }

        public virtual ICollection<UserReminderSetting> UserReminderSettings { get; set; }
    }
}