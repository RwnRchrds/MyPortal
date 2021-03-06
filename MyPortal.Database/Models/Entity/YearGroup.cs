using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("YearGroups")]
    public partial class YearGroup : BaseTypes.Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public YearGroup()
        {
            RegGroups = new HashSet<RegGroup>();
        }
        
        [Column(Order = 1)] 
        public Guid StudentGroupId { get; set; }

        [Column(Order = 4)]
        public Guid CurriculumYearGroupId { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }
        public virtual CurriculumYearGroup CurriculumYearGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegGroup> RegGroups { get; set; }
    }
}
