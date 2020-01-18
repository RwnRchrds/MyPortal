using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Data.Models
{
    [Table("DetentionType")]
    public class DetentionType
    {
        public DetentionType()
        {
            Detentions = new HashSet<Detention>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public virtual ICollection<Detention> Detentions { get; set; }
    }
}
