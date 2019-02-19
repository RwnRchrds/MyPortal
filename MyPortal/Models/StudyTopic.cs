using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyPortal.Models
{
    [Table("Curriculum_StudyTopics")]
    public class StudyTopic
    {
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public StudyTopic()
        {
            LessonPlans = new HashSet<LessonPlan>();
        }
        
        [Display(Name="ID")]
        public int Id { get; set; }

        [Display(Name = "Subject")]
        public int SubjectId { get; set; }

        [Display(Name = "Year Group")]
        public int YearGroupId { get; set; }

        [Required] 
        public string Name { get; set; }
        
        public virtual Subject Subject { get; set; }

        public virtual YearGroup YearGroup { get; set; }
        
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonPlan> LessonPlans { get; set; }
    }
}