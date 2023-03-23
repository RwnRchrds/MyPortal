using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("YearGroups")]
    public partial class YearGroup : BaseTypes.Entity, IStudentGroupEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public YearGroup()
        {
            RegGroups = new HashSet<RegGroup>();
        }
        
        [Column(Order = 2)] 
        public Guid StudentGroupId { get; set; }

        [Column(Order = 3)]
        public Guid CurriculumYearGroupId { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }
        public virtual CurriculumYearGroup CurriculumYearGroup { get; set; }

        
        public virtual ICollection<RegGroup> RegGroups { get; set; }
    }
}
