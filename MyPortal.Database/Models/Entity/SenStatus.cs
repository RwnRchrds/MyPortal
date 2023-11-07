using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("SenStatus")]
    public class SenStatus : LookupItem, ICensusEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SenStatus()
        {
            Students = new HashSet<Student>();
        }

        [Column(Order = 4)]
        [Required]
        [StringLength(1)]
        public string Code { get; set; }


        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<SenReview> SenReviews { get; set; }
    }
}