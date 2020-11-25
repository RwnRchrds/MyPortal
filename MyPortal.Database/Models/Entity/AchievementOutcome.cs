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
            Achievements = new HashSet<Achievement>();
        }

        [Column (Order = 3)]
        public bool System { get; set; }

        public virtual ICollection<Achievement> Achievements { get; set; }
    }
}
