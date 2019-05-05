namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Curriculum_Subjects")]
    public partial class CurriculumSubject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CurriculumSubject()
        {
            AssessmentResults = new HashSet<AssessmentResult>();
            CurriculumClasses = new HashSet<CurriculumClass>();
            CurriculumStudyTopics = new HashSet<CurriculumStudyTopic>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public int LeaderId { get; set; }

        [Required]
        [StringLength(5)]
        public string Code { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssessmentResult> AssessmentResults { get; set; }

        public virtual PeopleStaffMember Leader { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurriculumClass> CurriculumClasses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurriculumStudyTopic> CurriculumStudyTopics { get; set; }
    }
}
