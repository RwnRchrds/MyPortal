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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ResultSetId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubjectId { get; set; }

        [Required]
        [StringLength(255)]
        public string Value { get; set; }

        public virtual AssessmentResultSet AssessmentResultSet { get; set; }

        public virtual PeopleStudent PeopleStudent { get; set; }

        public virtual CurriculumSubject CurriculumSubject { get; set; }
    }
}
