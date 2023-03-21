using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("CurriculumGroups")]
    public class CurriculumGroup : BaseTypes.Entity
    {
        public CurriculumGroup()
        {
            Classes = new HashSet<Class>();
        }

        [Column(Order = 2)]
        public Guid BlockId { get; set; }
        
        [Column(Order = 3)] 
        public Guid StudentGroupId { get; set; }

        public virtual CurriculumBlock Block { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
    }
}