using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("AchievementOutcomes")]
    public class AchievementOutcome : LookupItem
    {
        public AchievementOutcome()
        {
            StudentAchievements = new HashSet<StudentAchievement>();
        }

        public virtual ICollection<StudentAchievement> StudentAchievements { get; set; }
    }
}