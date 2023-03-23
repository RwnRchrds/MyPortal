using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("AchievementTypes")]
    public class AchievementType : LookupItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AchievementType()
        {
            Achievements = new HashSet<Achievement>();
        }

        [Column(Order = 4)]
        public int DefaultPoints { get; set; }

        
        public virtual ICollection<Achievement> Achievements { get; set; }
    }
}