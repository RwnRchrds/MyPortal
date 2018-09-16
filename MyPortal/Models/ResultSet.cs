using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MyPortal.Models
{
    public class ResultSet
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ResultSet()
        {
            Results = new HashSet<Result>();
        }

        [Display(Name = "ID")] 
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Is Current")] 
        public bool IsCurrent { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }
    }
}