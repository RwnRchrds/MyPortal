using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Database
{
    /// <summary>
    /// Category of achievement.
    /// </summary>
    [Table("Behaviour_AchievementTypes")]
    public class BehaviourAchievementType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BehaviourAchievementType()
        {
            Achievements = new HashSet<BehaviourAchievement>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public int DefaultPoints { get; set; }

        public bool System { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BehaviourAchievement> Achievements { get; set; }
    }
}