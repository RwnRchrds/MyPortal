using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("AchievementOutcomes")]
    public class AchievementOutcome : LookupItem, ISystemEntity
    {
        public AchievementOutcome()
        {
            StudentAchievements = new HashSet<StudentAchievement>();
        }

        [Column (Order = 3)]
        public bool System { get; set; }

        public virtual ICollection<StudentAchievement> StudentAchievements { get; set; }
    }
}
