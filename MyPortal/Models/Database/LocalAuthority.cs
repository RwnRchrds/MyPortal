using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Database
{
    [Table("System_LocalAuthorities")]
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