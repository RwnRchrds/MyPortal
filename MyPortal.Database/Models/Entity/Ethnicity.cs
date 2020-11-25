using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
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
