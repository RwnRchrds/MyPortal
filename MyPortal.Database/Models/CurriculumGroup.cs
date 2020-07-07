using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("CurriculumGroup")]
    public class CurriculumGroup
    {
        public CurriculumGroup()
        {
            Classes = new HashSet<Class>();
        }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public Guid Id { get; set; }
        
        [Column(Order = 1)]
        public Guid BlockId { get; set; }
        
        [Column(Order = 2)]
        [StringLength(10)]
        public string Code { get; set; }
        
        [Column(Order = 3)]
        [StringLength(256)]
        public string Description { get; set; }

        public virtual CurriculumBlock Block { get; set; }
        
        public virtual ICollection<CurriculumGroupMembership> Memberships { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
    }
}