namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Assessment_Results")]
    public partial class AssessmentResult
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name="Result Set")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ResultSetId { get; set; }

        [Key]
        [Column(Order = 1)]
        [Display(Name="Student")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentId { get; set; }

        [Key]
        [Display(Name="Subject")]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubjectId { get; set; }

        [Required]
        [StringLength(255)]
        public string Value { get; set; }

        public virtual AssessmentResultSet AssessmentResultSet { get; set; }

        public virtual Student Student { get; set; }

        public virtual CurriculumSubject CurriculumSubject { get; set; }
    }
}
