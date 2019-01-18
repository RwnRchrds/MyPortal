using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MyPortal.Models
{
    public class Subject
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subject()
        {
            Results = new HashSet<Result>();
        }

        [Display(Name = "ID")] public int Id { get; set; }

        [Required] [StringLength(255)] public string Name { get; set; }

        [Display(Name = "Head of Department")] public int LeaderId { get; set; }

        [Required] public string Code { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }

        public virtual Staff Staff { get; set; }
    }
}