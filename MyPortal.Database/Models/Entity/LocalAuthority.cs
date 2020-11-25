using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("LocalAuthorities")]
    public class LocalAuthority : BaseTypes.Entity
    {
        public LocalAuthority()
        {
            Schools = new HashSet<School>();
        }

        [Column(Order = 1)]
        public int LeaCode { get; set; }

        [Column(Order = 2)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 3)]
        public string Website { get; set; }

        public virtual ICollection<School> Schools { get; set; }
    }
}