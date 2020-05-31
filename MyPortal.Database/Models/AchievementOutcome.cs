using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("AchievementOutcome")]
    public class AchievementOutcome : LookupItem
    {
        public AchievementOutcome()
        {
            Achievements = new HashSet<Achievement>();
        }

        public bool System { get; set; }

        public virtual ICollection<Achievement> Achievements { get; set; }
    }
}
