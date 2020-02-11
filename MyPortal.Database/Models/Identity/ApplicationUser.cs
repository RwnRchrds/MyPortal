using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace MyPortal.Database.Models.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            ProfileLogNotes = new HashSet<ProfileLogNote>();
            Documents = new HashSet<Document>();
            MedicalEvents = new HashSet<MedicalEvent>();
            Incidents = new HashSet<Incident>();
            Achievements = new HashSet<Achievement>();
            LessonPlans = new HashSet<LessonPlan>();
            Bulletins = new HashSet<Bulletin>();
        }

        public bool Enabled { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<ProfileLogNote> ProfileLogNotes { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        public virtual ICollection<MedicalEvent> MedicalEvents { get; set; }

        public virtual ICollection<Incident> Incidents { get; set; }

        public virtual ICollection<Achievement> Achievements { get; set; }

        public virtual ICollection<LessonPlan> LessonPlans { get; set; }

        public virtual ICollection<Bulletin> Bulletins { get; set; }

        public string GetDisplayName(bool salutationFormat)
        {
            return Person != null ? Person.GetDisplayName(salutationFormat) : UserName;
        }
    }
}
