using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("Ethnicities")]
    public class Ethnicity : LookupItem
    {
        public Ethnicity()
        {
            People = new HashSet<Person>();
        }

        [Column(Order = 3)]
        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
