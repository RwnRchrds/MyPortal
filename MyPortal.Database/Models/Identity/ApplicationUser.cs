using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace MyPortal.Database.Models.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            ProfileLogNotes = new HashSet<LogNote>();
            Documents = new HashSet<Document>();
            MedicalEvents = new HashSet<MedicalEvent>();
            Incidents = new HashSet<Incident>();
            Achievements = new HashSet<Achievement>();
            LessonPlans = new HashSet<LessonPlan>();
            Bulletins = new HashSet<Bulletin>();
            AssignedBy = new HashSet<Task>();
        }

        [StringLength(5)]
        public string UserType { get; set; }

        public Guid? SelectedAcademicYearId { get; set; }

        public bool Enabled { get; set; }

        public virtual Person Person { get; set; }

        public virtual AcademicYear SelectedAcademicYear { get; set; }

        public virtual ICollection<LogNote> ProfileLogNotes { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        public virtual ICollection<MedicalEvent> MedicalEvents { get; set; }

        public virtual ICollection<Incident> Incidents { get; set; }

        public virtual ICollection<Achievement> Achievements { get; set; }

        public virtual ICollection<LessonPlan> LessonPlans { get; set; }

        public virtual ICollection<Bulletin> Bulletins { get; set; }

        public virtual ICollection<Task> AssignedBy { get; set; }
    }
}
