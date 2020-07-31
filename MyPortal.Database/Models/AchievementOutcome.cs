using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("AchievementOutcomes")]
    public class AchievementOutcome : LookupItem
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
