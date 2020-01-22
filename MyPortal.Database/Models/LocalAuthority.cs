using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("LocalAuthority")]
    public class LocalAuthority
    {
        public LocalAuthority()
        {
            Schools = new HashSet<School>();
        }

        public int Id { get; set; }

        public int LeaCode { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public string Website { get; set; }

        public virtual ICollection<School> Schools { get; set; }
    }
}