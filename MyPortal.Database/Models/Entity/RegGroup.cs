using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("RegGroups")]
    public partial class RegGroup : BaseTypes.Entity
    {
        [Column(Order = 1)] 
        public Guid StudentGroupId { get; set; }

        [Column(Order = 3)]
        public Guid YearGroupId { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }

        public virtual YearGroup YearGroup { get; set; }
    }
}
