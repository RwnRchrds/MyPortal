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

        public DateTime Date { get; set; }

        [StringLength(50)]
        public string Comments { get; set; }

        public int Points { get; set; }

        public bool Resolved { get; set; }

        public bool Deleted { get; set; }

        public virtual BehaviourAchievementType BehaviourAchievementType { get; set; }

        public virtual BehaviourLocation BehaviourLocation { get; set; }

        public virtual CurriculumAcademicYear CurriculumAcademicYear { get; set; }

        public virtual StaffMember RecordedBy { get; set; }

        public virtual Student Student { get; set; }
    }
}