using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Database
{
    // SYSTEM CLASS -- LOOKUP ONLY

    [Table("Assessment_AspectTypes")]
    public class AssessmentAspectType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AssessmentAspectType()
        {
            Aspects = new HashSet<AssessmentAspect>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssessmentAspect> Aspects { get; set; }
    }
}