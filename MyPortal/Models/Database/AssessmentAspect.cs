using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Database
{
    [Table("Assessment_Aspects")]
    public class AssessmentAspect
    {
        public AssessmentAspect()
        {
            Results = new HashSet<AssessmentResult>();
        }

        public int Id { get; set; }
        public int TypeId { get; set; }
        public int? GradeSetId { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual AssessmentAspectType AspectType { get; set; }
        public virtual AssessmentGradeSet GradeSet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssessmentResult> Results { get; set; }
    }
}