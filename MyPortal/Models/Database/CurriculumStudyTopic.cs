namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A topic for study in the curriculum. A study topic contains lesson plans for delivery.
    /// </summary>
    [Table("Curriculum_StudyTopics")]
    public partial class CurriculumStudyTopic
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public int YearGroupId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public virtual CurriculumSubject CurriculumSubject { get; set; }

        public virtual PastoralYearGroup PastoralYearGroup { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurriculumLessonPlan> LessonPlans { get; set; }
    }
}
