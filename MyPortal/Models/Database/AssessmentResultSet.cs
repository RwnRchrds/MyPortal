namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Assessment_ResultSets")]
    public partial class AssessmentResultSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AssessmentResultSet()
        {
            AssessmentResults = new HashSet<AssessmentResult>();
        }

        public int Id { get; set; }

        [Display(Name="Academic Year")]
        public int AcademicYearId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name="Is Current")]
        public bool IsCurrent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssessmentResult> AssessmentResults { get; set; }

        public virtual CurriculumAcademicYear CurriculumAcademicYear { get; set; }
    }
}
