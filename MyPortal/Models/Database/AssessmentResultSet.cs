namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A set of results awarded to students. Result sets usually represent a time-frame eg 'Spring Term'.
    /// </summary>
    [Table("Assessment_ResultSets")]
    public class AssessmentResultSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AssessmentResultSet()
        {
            Results = new HashSet<AssessmentResult>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsCurrent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssessmentResult> Results { get; set; }
    }
}
