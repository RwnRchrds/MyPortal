using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Database
{
    /// <summary>
    /// Record of a student achievement.
    /// </summary>
    [Table("Behaviour_Achievements")]
    public class BehaviourAchievement
    {
        public int Id { get; set; }

        public int AcademicYearId { get; set; }

        public int AchievementTypeId { get; set; }

        public int StudentId { get; set; }

        public int LocationId { get; set; }

        public int RecordedById { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public string Comments { get; set; }

        public int Points { get; set; }

        public bool Resolved { get; set; }

        public bool Deleted { get; set; }

        public virtual BehaviourAchievementType Type { get; set; }

        public virtual SchoolLocation Location { get; set; }

        public virtual CurriculumAcademicYear AcademicYear { get; set; }

        public virtual StaffMember RecordedBy { get; set; }

        public virtual Student Student { get; set; }
    }
}