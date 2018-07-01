using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Models
{
    public partial class LogType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LogType()
        {
            Logs = new HashSet<Log>();
        }

        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Logs { get; set; }
    }
}
