namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A class in which students are enrolled.
    /// </summary>
    [Table("Curriculum_Classes")]
    public partial class CurriculumClass
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CurriculumClass()
        {
            Enrolments = new HashSet<CurriculumEnrolment>();
            Sessions = new HashSet<CurriculumSession>();
        }

        public int Id { get; set; }

        public int AcademicYearId { get; set; }

        public int? SubjectId { get; set; }

        [Required]
        public string Name { get; set; }

        public int TeacherId { get; set; }

        public int? YearGroupId { get; set; }

        public virtual StaffMember Teacher { get; set; }

        public virtual CurriculumAcademicYear CurriculumAcademicYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurriculumEnrolment> Enrolments { get; set; }

        public virtual CurriculumSubject CurriculumSubject { get; set; }

        public virtual PastoralYearGroup PastoralYearGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurriculumSession> Sessions { get; set; }
    }
}
