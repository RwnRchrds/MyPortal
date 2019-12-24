using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    /// <summary>
    /// A topic for study in the curriculum. A study topic contains lesson plans for delivery.
    /// </summary>
    [Table("StudyTopic", Schema = "curriculum")]
    public class StudyTopic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudyTopic()
        {
            LessonPlans = new HashSet<LessonPlan>();
        }

        public int Id { get; set; }

        public int SubjectId { get; set; }

        public int YearGroupId { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual YearGroup YearGroup { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonPlan> LessonPlans { get; set; }
    }
}